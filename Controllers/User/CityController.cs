using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DK_API.Controllers.Temp;
using DK_API.Enumerator;
using DK_API.Dtos;
using DK_API.Entities;
using DK_API.Repository;
using DK_API.Repository.InfRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace DK_API.Controllers
{
    /// <summary>
    /// Controller City GetData info for City
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _repository;
        private readonly MongodbIpGeoLocation _ipLocRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<CityController> _logger;
        public CityController(ICityRepository repository, IMapper mapper,
            IHttpClientFactory factory,
            ILogger<CityController> logger,
            MongodbIpGeoLocation ipLocRepository)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._factory = factory;
            this._logger = logger;
            this._ipLocRepository = ipLocRepository;
        }
        /// <summary>
        /// Dữ Liệu Thành Phố
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/City
        /// 
        /// Sample response:
        /// 
        ///     {
        ///       "code": 11,
        ///       "city_focussed": -1,
        ///       "city": [
        ///         {
        ///           "cityId": 1,
        ///           "name": "Hà Nội"
        ///         },
        ///     ....
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Trả về mảng Thành Phố</response>
        [HttpGet]
        public async Task<ActionResult<Object>> GetCitysAsync()
        {
            char[] splits = { ' ', '-' };
            int focussed = 0;
            string ip = null;
            List<CityDto> items = (await _repository.GetCitysAsync())
                .Select(city => _mapper.Map<CityDto>(city)).ToList();

            foreach (var header in Request.Headers)
            {
                if (header.Key.Equals("X-Real-IP"))
                {
                    ip = header.Value;
                    break;
                }
            }
            try
            {
                focussed = -1;
                IpInfoEntity myDeserializedClass = null;
                HttpResponseMessage response = null;
                if (!string.IsNullOrEmpty(ip) && !Extension.IsLocalIpAddress(ip))
                {
                    IpGeolocation loc = await _ipLocRepository.GetOneByIpv4Async(ip);
                    if (loc == null)
                    {
                        HttpClient client = _factory.CreateClient();
                        client.BaseAddress = new Uri($"https://ipinfo.io/{ip}/json?token=c96455162fca4e");
                        response = await client.GetAsync("");

                        if (response != null && response.IsSuccessStatusCode)
                        {
                            string jsonData = response.Content.ReadAsStringAsync().Result;
                            myDeserializedClass = JsonSerializer.Deserialize<IpInfoEntity>(jsonData);

                            string region = myDeserializedClass.region;
                            string cityName = myDeserializedClass.city;
                            string country = myDeserializedClass.country;

                            string regionName = Extension.RemoveCharInString(region, splits);
                            var city = (await _repository.GetCityByNameLowerAsync(regionName.Trim()));

                            loc = _mapper.Map<IpGeolocation>(myDeserializedClass);
                            if (city != null)
                            {
                                focussed = city.CityId;
                                loc.cityId = city.CityId;
                            }
                            loc.UpdatedDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                            if (string.IsNullOrEmpty(loc.CreatedDate))
                                loc.CreatedDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                            await _ipLocRepository.CreateOneAsync(loc);
                        }
                    }
                    else
                    {
                        focussed = loc.cityId.Equals(0) ? -1 : (loc.cityId);
                    }
                    _logger.LogInformation($"ClientIP {loc.ip} request city \"{loc.city}\" region \"{loc.region}\" location \"{loc.loc}\"");
                }

            }
            catch (System.Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
                return StatusCode(200, new
                {
                    code = ECodeResp.internalError,
                    city_focussed = focussed,
                    city = items
                });
            }
            return StatusCode(200, new
            {
                code = ECodeResp.Info,
                city_focussed = focussed,
                city = items
            });
        }

        /// <summary>
        /// Tên thành phố theo Id thành phố
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/City/{idCity}
        /// 
        /// </remarks>
        /// <param name="cityId">Id Thành Phố</param>
        /// <returns>tên thành phố</returns>
        /// <response code="200">Trả về tên thành phố hoặc null nếu không tìm thấy</response>
        [HttpGet("{cityId}")]
        public async Task<Object> GetCitysByCityIdAsync(int cityId)
        {
            var items = (await _repository.GetCitysAsync()).Where(c => c.CityId == cityId)
                    .Select(city => city.Name).SingleOrDefault();
            if (items is null)
                return StatusCode(200, new { code = ECodeResp.NotFound, message = "không tìm thấy tên thành phố" });
            return StatusCode(200, new { code = ECodeResp.Info, city = items });
        }
    }
}