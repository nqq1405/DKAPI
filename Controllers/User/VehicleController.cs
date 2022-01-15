using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DK_API.Dtos;
using DK_API.Entities;
using DK_API.Repository;
using DK_API.Repository.InfRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DK_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleController> _logger;
        public VehicleController(IVehicleRepository repository, IMapper mapper,
        ILogger<VehicleController> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Danh sách loại phương tiện theo loại
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/vehicle?{vehicleId}
        ///         True => Danh sách Phương tiện đường Bộ
        ///         false => Danh sách Phương tiện đăng kiểm (default)
        /// 
        /// </remarks>
        /// <param name="vehicleType">
        ///     Loại Phương Tiện (True or False)
        /// </param>
        /// <returns>Danh sách Phương tiện theo loại</returns>
        /// <response code="200">Trả về Mảng Object phương tiện</response>
        [HttpGet("{vehicleType}")]
        public async Task<IEnumerable<Object>> GetVehiclesAsync([Required] bool vehicleType = false)
        {
            var ldb = (await _repository.GetVehiclesByVehicleTypeAsync("LDB")).Select(vehicle => _mapper.Map<VehicleDto>(vehicle));
            var ldk = (await _repository.GetVehiclesByVehicleTypeAsync("LDK")).Select(vehicle => _mapper.Map<VehicleDto>(vehicle));
            if (vehicleType) return ldb;
            return ldk;
        }

        /// <summary>
        /// Tên theo mã phương tiện và loại phương tiện
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/Vehicle/name/ldb/12
        /// 
        /// </remarks>
        /// <param name="vehicleId">Mã phương tiện</param>
        /// <returns>Tên loại phương tiện</returns>
        /// <response code="200">Tên phương tiện || null nếu Id tồn tại || Invalid vehicle type</response>
        [HttpGet("name/{vehicleId}")]
        public async Task<ActionResult<Object>>
            GetVehicleNameByVehicleIdAsync([Required] int vehicleId)
        {
            var vehicleName = (await _repository.GetVehiclesAsync())
                .Where(v => v.VehicleId == vehicleId)
                .Select(v => v.NameVehicle).SingleOrDefault();
            if (vehicleName is null) return StatusCode(200, new { code = 44, message = "Không tìm thấy tên xe" });
            return StatusCode(200, new { Name = vehicleName });
        }
    }

}