using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;
using DK_API.Repository.InfRepository;
using Microsoft.AspNetCore.Mvc;
using DK_API.Service;
using DK_API.Enumerator;
using Microsoft.Extensions.Logging;

namespace DK_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotDataRepository _repository;
        private readonly SlotService _service;
        private readonly ILogger<SlotController> _logger;

        public SlotController(ISlotDataRepository repository,
            SlotService service,
            ILogger<SlotController> logger)
        {
            this._repository = repository;
            this._service = service;
            this._logger = logger;
        }

        /// <summary>
        /// Slot đặt lịch theo ngày
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/slot
        /// 
        /// </remarks>
        /// <param name="datetime">yyyy-MM-DDT00:00:00.000000</param>
        /// <param name="stationId">id trạm đăng kiểm</param>
        /// <returns>
        /// Array slot
        /// </returns>
        /// <response code="200">array slot</response>
        [HttpGet]
        public async Task<ActionResult<Object>> GetTimeSlotsAsync(
            [Required][FromQuery] DateTime datetime,
            [Required][FromQuery] int stationId)
        {
            try
            {
                if (datetime.IsDatePast())
                {
                    return StatusCode(200, new { code = ECodeResp.Info, message = "Thời giạn chọn đã quá thời gian hiện tại" });
                }

                var data = await _service.GetSlotByDateTimeAsync(datetime, stationId);

                return StatusCode(200, new
                {
                    code = ECodeResp.Info,
                    slot = data
                });
            }
            catch (System.Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
                return StatusCode(200, new
                {
                    code = ECodeResp.Info,
                    slot = await _service.GetSlotByDateTimeAsync(datetime, stationId)
                });
            }
        }

        /// <summary>
        /// thời gian lấy xe tại nhà
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/slot/slot_home
        /// 
        /// </remarks>
        /// <param name="datetime">yyyy-MM-DDT00:00:00.000000</param>
        /// <returns>
        /// Array string time
        /// </returns>
        /// <response code="200">array time</response>
        [HttpGet("slot_home")]
        public async Task<ActionResult<Object>> GetTimeSlotHomeAsync(
                [Required][FromQuery] DateTime datetime)
        {
            if (datetime.IsDatePast())
                return StatusCode(200, new { code = ECodeResp.Info, message = "Thời giạn chọn đã quá thời gian hiện tại" });
            var slotData = await _repository.GetTimeAsync((int)datetime.DayOfWeek);

            var slot = slotData.status ? slotData.slotLists.Where(s => s.status).Select(s => s.Time) : null;
            if (slot is null)
                return StatusCode(200, new { code = ECodeResp.Info, message = "Ngày bạn chọn là ngày nghỉ" });

            return StatusCode(200, new { code = ECodeResp.Info, data = slot });
        }


    }
}