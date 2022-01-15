using System.Threading.Tasks;
using DK_API.Entities;
using DK_API.Repository.InfRepository;
using Microsoft.AspNetCore.Mvc;
using DK_API.Dtos.RegisCost;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DK_API.Enumerator;
using DK_API.Dtos.RoadCost;
using DK_API.Repository;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DK_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IPDBRepository _pdbRepository;
        private readonly IPKDRepository _pkdRepository;
        private readonly ILogger<PriceController> _logger;
        private readonly IMapper _mapper;
        public PriceController(IPDBRepository pdbRepository,
            IPKDRepository pkdRepository,
            IVehicleRepository vehicleRePo,
            IMapper mapper,
            ILogger<PriceController> logger)
        {
            this._pdbRepository = pdbRepository;
            this._pkdRepository = pkdRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Thông tin giá theo loại xe đăng kiểm (for DEV)
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///         GET /api/Price
        ///         
        /// </remarks>
        /// 
        /// <returns>Object</returns>
        /// <response code="200">OK</response>
        [HttpGet]
        public async Task<ActionResult<Object>> getAllVehiclePrice()
        {
            var pdbs = await _pdbRepository.GetPDBsAsync();
            var pkds = await _pkdRepository.GetPKDsAsync();
            return StatusCode(200, new
            {
                code = ECodeResp.Info,
                pkdlist = pkds.Select(s => _mapper.Map<RegisCostDto>(s)),
                pdblist = pdbs.Select(s => _mapper.Map<RoadCostDto>(s))
            });
        }


        /// <summary>
        /// báo giá phương tiện theo Id của phương tiện 
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/Price/{vehicleId} 
        ///     -> loại phương tiện đường bộ vehicleId(12-19)
        ///     -> loại phương tiện đăng kiểm vehicleId(1-11)
        ///
        /// Sample response:
        /// 
        ///     {
        ///       [
        ///           {
        ///             "vehicleId": 12,
        ///             "typeVehicle": "LDB",
        ///             "nameVehicle": "Xe chở người dưới 10 chỗ đăng ký tên cá nhân"
        ///           },
        ///           ....
        ///           {...}
        ///        ]
        ///     }
        /// 
        /// </remarks>
        /// <param name="vehicleId"> Id của loại phương tiên </param>
        /// <returns></returns>
        /// <response code="200">Fake status code không tìm thấy</response>
        [HttpGet("{vehicleId}")]
        public async Task<ActionResult<Object>> GetPriceAsync(int vehicleId)
        {

            if (vehicleId > 0)
            {
                if (vehicleId < 12)
                {
                    var pkd = await _pkdRepository.GetPKDAsync(vehicleId);
                    if (pkd is null)
                        return StatusCode(200, new { code = ECodeResp.NotFound, message = "Không tìm thấy thông tin phí đăng kiểm" });

                    return _mapper.Map<RegisCostDto>(pkd);
                }
                else if (vehicleId < 20)
                {
                    var pdb = await _pdbRepository.GetPDBAsync(vehicleId);
                    if (pdb is null)
                        return StatusCode(200, new { code = ECodeResp.NotFound, message = "Không tìm thấy thông tin phí đường bộ" });

                    return _mapper.Map<RoadCostDto>(pdb);
                }
            }
            return StatusCode(200, new { code = ECodeResp.NotFound, message = "Không tìm thấy thông tin" });
        }

        // Put /PKD/{id}
        /// <summary>
        /// test Cập nhật giá kiểm định
        /// </summary>
        /// <param name="idVehicle">Id của loại xe</param>
        /// <param name="RcDto">thân của Pkd: phí đăng kiểm {PriceKD: (int), PriceCert: (int)}</param>
        /// <returns></returns>
        /// <response code="201">no content</response>
        /// <response code="200">ok</response>
        [HttpPut("pkd/{idVehicle}")]
        public async Task<ActionResult> UpdatePKDAsync([Required][Range(1, 11)] int idVehicle, [FromBody] RegisCostUpdate RcDto)
        {
            try
            {
                var existingPKD = await _pkdRepository.GetPKDAsync(idVehicle);
                if (existingPKD is null)
                    return StatusCode(200, new { code = ECodeResp.NotFound, message = "không tìm thấy thông tin đăng kiểm" });

                RegisCost updatePKD = _mapper.Map<RegisCost>(RcDto);
                if (string.IsNullOrEmpty(updatePKD.CreatedDate))
                    updatePKD.CreatedDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

                updatePKD.VehicleId = idVehicle;
                updatePKD._id = existingPKD._id;
                updatePKD.UpdatedDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                _logger.LogInformation($@"Thông tin giá kiểm định đã được cập nhật mã xe:{idVehicle} với giá kiểm định {existingPKD.PriceKD} => {updatePKD.PriceKD} | giá chứng chỉ {existingPKD.PriceCert} => {updatePKD.PriceCert}");
                await _pkdRepository.UpdatePKDAsync(updatePKD);
            }
            catch (System.Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
            }
            return NoContent();
        }

        /// <summary>
        /// test Cập nhật giá kiểm định
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///     
        ///     PUT: /api/Price/pdb/{idVehicle}
        /// 
        /// </remarks>
        /// <param name="idVehicle">Mã của Phương tiện</param>
        /// <param name="RegisCost">thân của Pkd: phí đăng kiểm {PriceDB: (int)}</param>
        /// <returns></returns>
        /// <response code="201">no content</response>
        /// <response code="200">không tìm thấy phí đăng kiểm của vehicleId</response>
        [HttpPut("pdb/{idVehicle}")]
        public async Task<ActionResult> UpdatePDBAsync([Required][Range(12, 19)] int idVehicle, [FromBody] RoadCostUpdate RegisCost)
        {
            try
            {
                var existingPDB = await _pdbRepository.GetPDBAsync(idVehicle);
                if (existingPDB is null)
                    return StatusCode(200, new { code = ECodeResp.NotFound, message = "Không tìm thấy" });
                RoadCost updatePDB = _mapper.Map<RoadCost>(RegisCost);
                updatePDB.VehicleId = idVehicle;
                updatePDB._id = existingPDB._id;
                await _pdbRepository.UpdatePDBAsync(updatePDB);
                _logger.LogInformation($@"Thông tin giá kiểm định đã được cập nhật mã xe:{idVehicle} với phí đường bộ {existingPDB.PriceDB} => {updatePDB.PriceDB}");

            }
            catch (System.Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
            }
            return NoContent();
        }

    }
}