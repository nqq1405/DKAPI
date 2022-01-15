using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using  DK_API .Enumerator;
using  DK_API .Dtos.Voucher;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using  DK_API .Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace  DK_API .Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _repository;
        private readonly VoucherService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<VoucherController> _logger;


        public VoucherController(IVoucherRepository repository, 
        IMapper mapper, 
        VoucherService service, ILogger<VoucherController> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._service = service;
            this._logger = logger;
        }

        /// <summary>
        /// Kiểm tra thông tin voucher
        /// </summary>
        /// <param name="voucherCode">Mã voucher(code)</param>
        /// <returns>StatusCode</returns>
        /// <response code="200"> code: 44, message = "không tìm thấy voucher" || 
        ///             code: "11het", message = "Voucher đã hết slot" ||
        ///             code = 11 , message = "Có thông tin voucher"
        /// </response>
        [HttpGet("is_voucher/{voucherCode}")]
        public async Task<ActionResult> IsVoucher(string voucherCode){
            if(!await _service.IsVoucher(voucherCode))
                return StatusCode(200, new {code =  ECodeResp.NotFound , message = "không tìm thấy voucher"});
            
            if(!await _service.CheckCountVoucher(voucherCode))
                return StatusCode(200, new {code =  $"{(int)ECodeResp.Info}het" , message = "Voucher đã hết slot"});
            return StatusCode(200, new {code =  ECodeResp.Info , message = "Có voucher"});
        }

        /// <summary>
        /// Lấy toàn bộ thông tin voucher
        /// </summary>
        /// <returns>Mảng voucher</returns>
        [HttpGet]
        public async Task<Object> GetAllVouchersAsync()
        {
            var data = (await _repository.GetVouchersAsync())
                .Select(vou => _mapper.Map<VoucherDto>(vou));
                
            if(data is null || data.Count()==0)
                return StatusCode(200, new {
                        code =  ECodeResp.NotFound, 
                        message = "không tìm thấy voucher"});

            return data;
        }

        /// <summary>
        /// Tạo Mã voucher
        /// </summary>
        /// <param name="v">Body VoucherCreateDto see Schema</param>
        /// <returns></returns>
        /// <response code="204"></response>
        [HttpPost]
        public async Task<ActionResult> CreateVoucherAsync([FromBody] VoucherCreate v)
        {
            if(await _service.existingVoucher(v.Code)) 
                return StatusCode(200 , new {code = ECodeResp.Exist, message = "Mã voucher đã tồn tại"});
            if(DateTime.Compare(v.StartTime, v.EndTime) > 0) 
                return StatusCode(200 , new {code = ECodeResp.Info, message = "Thời gian kết thúc phải sớm hơn thời gian bắt đầu"});
            Voucher voucher = _mapper.Map<Voucher>(v);
            await _repository.CreateVoucherAsync(voucher);
            return NoContent();
        }

        /// <summary>
        /// Cập nhật thông tin voucher 
        /// </summary>
        /// <param name="v">Body VoucherUpdateDto see Schema</param>
        /// <returns></returns>
        /// <response code="200"> code: 44, message = "không tìm thấy voucher" </response>
        [HttpPut]
        public async Task<ActionResult> UpdateVoucherAsync([FromBody] VoucherUpdate v)
        {
            var ExistVoucher = (await _repository.GetVouchersAsync())
                .ToList().Where(voucher => voucher.Code.Equals(v.Code)).SingleOrDefault();
            if (ExistVoucher is null) 
                return StatusCode(200, new {code = ECodeResp.NotFound , message = "không tìm thấy voucher" });
            Voucher voucher = ExistVoucher with
            {
                Code = v.Code,
                Description = v.Description,
                Count = v.Count,
                StartTime = v.StartTime,
                EndTime = v.EndTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                IsDisable = v.IsDisable,
                UpdatedDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
            };
            await _repository.UpdateVoucherAsync(voucher);
            return NoContent();
        }
    }
}