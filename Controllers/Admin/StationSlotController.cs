using Microsoft.AspNetCore.Mvc;
using DK_API.Repository.InterfaceRepo;
using AutoMapper;
using DK_API.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using DK_API.Repository.InfRepository;
using System.ComponentModel.DataAnnotations;
using DK_API.Enumerator;
using DK_API.Dtos.SlotDto;
using Microsoft.Extensions.Logging;

namespace DK_API.Controllers.admin
{
    /// <summary>
    /// For Admin
    /// </summary>
    [Produces("application/json")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StationSlotController : ControllerBase
    {
        private readonly IStationSlotDataRepository _repository;
        private readonly IMapper _mapper;
        private readonly StationService _stationService;
        private readonly SlotService _slotService;
        private readonly ILogger<StationSlotController> _logger;
        public StationSlotController(IStationSlotDataRepository repository,
            IMapper mapper, StationService service, SlotService slotService,
            ILogger<StationSlotController> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._stationService = service;
            this._slotService = slotService;
            this._logger = logger;
        }

        /// <summary>
        /// dữ liệu thông tin đặt lịch khách của các trạm
        /// </summary>
        /// <param name="_stationsId">Id của trạm</param>
        /// <returns>Array</returns>
        [HttpGet]
        public async Task<ActionResult<Object>> GetStationSlotDataAsync([Required] int _stationsId)
        {
            if (!_stationService.IsStationId(_stationsId))
                return StatusCode(200, new { code = ECodeResp.NotFound, message = "Không tìm thấy trạm này" });

            try
            {
                var data = (await _slotService.GetStationSlotDataByStationIdAsync(_stationsId));
                return StatusCode(200, new
                {
                    code = ECodeResp.Info,
                    infoSlotData = _mapper.Map<StationSlotDataDto>(data)
                });
            }
            catch (System.Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
                return StatusCode(200, new
                {
                    code = ECodeResp.Info,
                    infoSlotData = _mapper.Map<StationSlotDataDto>(_repository.GetStationSlotDataByStationIdAsync(_stationsId))
                });
            }
        }
    }
}