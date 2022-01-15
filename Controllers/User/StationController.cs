using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DK_API.Dtos;
using DK_API.Repository.InterfaceRepo;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DK_API.Service;
using DK_API.Enumerator;
using Microsoft.Extensions.Logging;

namespace DK_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationRepository _repository;
        private readonly IMapper _mapper;
        private readonly StationService _stationService;
        private readonly ILogger<StationController> _logger;

        public StationController(IStationRepository repository,
        IMapper mapper,
        StationService service,
        ILogger<StationController> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._stationService = service;
            this._logger = logger;
        }
        /// <summary>
        /// Danh sách Trạm đăng kiểm theo Id Thành phố
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/station?byCityId
        /// 
        /// </remarks>
        /// <param name="byCityId">Id Thành Phố</param>
        /// <returns></returns>
        /// <response code="200">Trả về Mảng Object Trạm Đk</response>
        [HttpGet("{byCityId}")]
        public async Task<ActionResult<Object>> GetStationsByCityIdAsync(int byCityId)
        {
            char[] trimchar = { ' ', ',', '-', '/', ';' };
            if (!_stationService.IsCityId(byCityId))
                return StatusCode(200, new { code = ECodeResp.NotFound, Message = "Không tìm thấy mã thành phố" });
            var items = (await _repository.GetStationsByCityIdAsync(byCityId))
                .Select(station => _mapper.Map<StationDto>(station))
                .ToList();
            items.ForEach(item =>
            {
                int index = item.StationName.LastIndexOf('/');
                item.Address = item.StationName.Substring(0, index).Trim(trimchar);
                item.StationName = item.StationName.Substring(index + 1).Trim(trimchar);
            });
            return items;
        }

        /// <summary>
        /// Tên Trạm đăng kiểm theo Id trạm DK
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/Station/5/station_name
        /// 
        /// </remarks>
        /// <param name="stationId">Id Trạm đăng kiểm</param>
        /// <returns>tên trạm đăng kiểm</returns>
        /// <response code="200">Trả về tên trạm đăng kiểm hoặc null nếu không tìm thấy</response>
        [HttpGet("name/{stationId}")]
        public async Task<Object> GetStationNameByStationIdAsync([Required] int stationId)
        {
            if (!_stationService.IsStationId(stationId))
                return StatusCode(200, new { code = ECodeResp.NotFound, Message = "Không tìm thấy mã trạm đăng kiểm" });

            var items = (await _repository.GetStationsAsync())
                .Where(x => x.StationId == stationId)
                .Select(station => station.StationName).SingleOrDefault();

            return new { Name = items };
        }

    }
}