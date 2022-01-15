using System.Collections.Generic;
using System.Threading.Tasks;
using DK_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DK_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        private readonly YearService _service;
        private readonly ILogger<YearController> _logger;
        public YearController(YearService service,ILogger<YearController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        /// <summary>
        /// Danh sách năm (tính từ hiện tại đến trong khoảng 25 năm gần nhất)
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /api/year
        /// 
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Trả về Mảng số năm (Int)</response>
        [HttpGet]
        public async Task<IEnumerable<int>> GetYearsAsync()
        {
            return await _service.GetYearsAsync();
        }
    }
}