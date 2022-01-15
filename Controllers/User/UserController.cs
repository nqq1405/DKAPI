using System;
using System.Threading.Tasks;
using AutoMapper;
using DK_API.Controllers.Temp;
using DK_API.Dtos.ScheduleUser;
using DK_API.Entities;
using DK_API.Repository.InfRepository;
using DK_API.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Berrysoft.XXTea;
using DK_API.Enumerator;
using Microsoft.Extensions.Logging;
using DK_API.Dtos.UserSchedule;

namespace DK_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly YearService _yearService;
        private readonly SlotService _slotService;
        private readonly VoucherService _voucherService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly string key = "s*d7&6@Dx6^%s2@#@4";
        private readonly IScheUserRepository _userRepository;
        private readonly IUserServiceOrderRepository _userServiceRepository;

        public UserController(UserService userService,
             YearService yearService,
             IScheUserRepository userRepository,
             IMapper mapper,
             SlotService timeService,
             VoucherService voucherService, ILogger<UserController> logger,
             IUserServiceOrderRepository userServiceRepository)
        {
            _userService = userService;
            _yearService = yearService;
            _userRepository = userRepository;
            _mapper = mapper;
            _slotService = timeService;
            _voucherService = voucherService;
            _logger = logger;
            _userServiceRepository = userServiceRepository;
        }

        /// <summary>
        /// chi phí tạm tính của người dùng đăng kiểm 
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Post /api/tempcosts
        ///  
        /// Sample response:
        /// 
        ///     {
        ///    "costPkd": "240.000 VNĐ",
        ///    "costCert": "100.000 VNĐ",
        ///    "costPdb": "780.000 VNĐ",
        ///    "costService": "1.000.000 VNĐ",
        ///    "costTotalTemp": "2.120.000 VNĐ"
        ///     }
        /// 
        /// </remarks>
        /// <param name="userTemp">xem thông tin body Schema ở cuốt trang</param>
        /// <returns>
        ///     Trả về Obj Kết quả Chi phí: 
        /// </returns>
        /// <response code="200">Return array City Info</response>
        /// <response code="200">năm không hợp lệ</response>
        [HttpPost]
        [Route("tempcosts")]
        public async Task<ActionResult<Object>> GetTempCostsAsync([FromBody] TempUserCostsInfo userTemp)
        {

            string ip = null;
            foreach (var header in Request.Headers)
            {
                if (header.Key.Equals("X-Real-IP")) ip = header.Value;
            }
            if (!string.IsNullOrEmpty(userTemp.Code) && !await _voucherService.IsVoucher(userTemp.Code))
                return StatusCode(200, new { code = ECodeResp.NotFound, message = "không tìm thấy thông tin voucher" });
            if (await _userService.checkingVoucherOneCar(userTemp.Code, userTemp.LicensePlates))
                return StatusCode(200, new { code = ECodeResp.Info, message = "Mã đã được sử dụng cho xe này" });
            if (await _yearService.Is25YearsNow(userTemp.year))
            {
                return StatusCode(200, new { message = $"năm {userTemp.year} không trong 25 năm trở lại" });
            }

            var tempcosts = await _userService.TempCostKDAsync(userTemp.vehiclePkdId, userTemp.vehiclePdbId,
                     userTemp.year, userTemp.uses, userTemp.service, userTemp.Code);

            return tempcosts;
        }

        /// <summary>
        /// Tạo user đặt lịch chỗ đăng kiểm
        /// </summary>
        /// <remarks>
        ///     Warnning:
        ///         
        ///         -> Thời gian và slot đặt lịch truyền lên api này Client cần qua bước chọn slot (lấy thông tin slot qua api '/api/Time')
        /// 
        ///     Sample request:
        /// 
        ///         POST api/user/schedule
        /// 
        ///     Sample response:
        /// </remarks>
        /// <param name="userCreate">Xem thông tin body ScheduleUserDtoCreate ở cuối trang</param>
        /// <returns>JSON</returns>
        /// <response code="400">name or PhoneNumber or LicensePlates InValid</response> 
        [HttpPost("schedule")]
        public async Task<ActionResult> CreateScheUserAsync([FromBody] UserScheduleCreate userCreate)
        {
            int year = userCreate.YearofManufacture;
            DateTime date = userCreate.Schedule;
            string timeslot = userCreate.TimeSlot;
            int vehiclePKDId = userCreate.vehiclePKDId;
            int vehiclePDBId = userCreate.vehiclePDBId;
            bool uses = userCreate.Uses;
            bool useService = userCreate.UseService;

            if (await _yearService.Is25YearsNow(year))
                return StatusCode(200, new { type = "year", code = ECodeResp.Info, message = "Năm không trong khoảng thời gian 25 năm tính từ năm hiện tại" });
            if (!_userService.CheckUserCityId(userCreate.CityId))
                return StatusCode(200, new { type = "city", code = ECodeResp.NotFound, message = "Không có thành phố này" });
            if (!_userService.CheckUserStationId(userCreate.StationId))
                return StatusCode(200, new { type = "station", code = ECodeResp.NotFound, message = "không có trạm này" });
            if (!_userService.CheckUserSlotString(timeslot))
                return StatusCode(200, new { type = "slot", code = ECodeResp.NotFound, message = "không có khung giờ này" });
            if (date.IsDatePast())
                return StatusCode(200, new { type = "time", code = ECodeResp.Info, message = "Thời gian đã quá hiện tại" });
            if (!(await _slotService.IsSlotStation(date, userCreate.StationId)))
                return StatusCode(200, new { type = "slot", code = ECodeResp.Info, message = $"khung giờ {date.ToString("yyyy'-'MM'-'dd")} có thể chưa có bạn tạo bằng api /api/slot ở bên trên" });
            if (await _slotService.CheckingDateSlotAsync(date, timeslot, userCreate.StationId))
                return StatusCode(200, new { type = "dateslot", code = ECodeResp.Info, message = "khung giờ này đã hết slot" });
            if (!string.IsNullOrEmpty(userCreate.VoucherCode) && !await _voucherService.IsVoucher(userCreate.VoucherCode))
                return StatusCode(200, new { type = "voucher", code = ECodeResp.NotFound, message = "không tìm thấy thông tin voucher" });
            if (!string.IsNullOrEmpty(userCreate.VoucherCode) && !await _voucherService.CheckCountVoucher(userCreate.VoucherCode))
                return StatusCode(200, new { type = "voucher", code = ECodeResp.Info, message = "Số lượng voucher này đã hết" });
            if (await _userService.checkingVoucherOneCar(userCreate.VoucherCode, userCreate.LicensePlates))
                return StatusCode(200, new { type = "voucher", code = ECodeResp.Info, message = "Mã đã được sử dụng cho xe này" });

            UserSchedule user = _mapper.Map<UserSchedule>(userCreate);
            try
            {
                user.infocosts = (InfoCosts)await _userService
                    .TempCostKDInfoTempAsync(vehiclePKDId,
                         vehiclePDBId, year, uses, useService, userCreate.VoucherCode);
                await _slotService.UpdateReduceScheduleSlotAsync(date, timeslot, userCreate.StationId);
                if (!string.IsNullOrEmpty(userCreate.VoucherCode))
                    await _voucherService.reduceCountVoucher(userCreate.VoucherCode);
                await _userRepository.CreateUserAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
                return Ok(new { code = ECodeResp.internalError, message = "Server có thể đang gặp vấn đề hày liên hệ người quản trị để được hỗ trợ" });
            }

            return Ok(new { objectId = user._id });
        }

        /// <summary>
        /// dặt dịch vụ đăng kiểm tại nhà
        /// </summary>
        /// <remarks>
        ///     Sample request:
        /// 
        ///         POST api/user/service_home
        /// 
        ///     Sample response:
        /// </remarks>
        /// <param name="user">Xem thông tin body UserServiceOrderCreate ở cuối trang</param>
        /// <returns>JSON</returns>
        /// <response code="400">name or PhoneNumber or LicensePlates InValid</response> 
        [HttpPost("home_service")]
        public async Task<ActionResult> CreateUserServiceHome(
            [FromBody] UserServiceOrderCreate user)
        {
            int year = user.YearofManufacture;
            DateTime date = user.Date;
            string time = user.Time;
            int vehiclePKDId = user.vehiclePKDId;
            int vehiclePDBId = user.vehiclePDBId;
            bool uses = user.Uses;

            if (await _yearService.Is25YearsNow(year))
                return StatusCode(200, new { type = "year", code = ECodeResp.Info, message = "Năm không trong khoảng thời gian 25 năm tính từ năm hiện tại" });
            if (!_userService.CheckUserCityId(user.CityId))
                return StatusCode(200, new { type = "city", code = ECodeResp.NotFound, message = "Không có thành phố này" });
            if (date.IsDatePast())
                return StatusCode(200, new { type = "time", code = ECodeResp.Info, message = "Thời gian đã quá hiện tại" });
            if (!string.IsNullOrEmpty(user.VoucherCode) && !await _voucherService.IsVoucher(user.VoucherCode))
                return StatusCode(200, new { type = "voucher", code = ECodeResp.NotFound, message = "không tìm thấy thông tin voucher" });
            if (!string.IsNullOrEmpty(user.VoucherCode) && !await _voucherService.CheckCountVoucher(user.VoucherCode))
                return StatusCode(200, new { type = "voucher", code = ECodeResp.Info, message = "Số lượng voucher này đã hết" });
            if (await _userService.checkingVoucherOneCar(user.VoucherCode, user.LicensePlates))
                return StatusCode(200, new { type = "voucher", code = ECodeResp.Info, message = "Mã đã được sử dụng cho xe này" });

            UserServiceOrder u = _mapper.Map<UserServiceOrder>(user);
            try
            {
                u.infocosts = (InfoCosts)await _userService
                    .TempCostKDInfoTempAsync(vehiclePKDId,
                         vehiclePDBId, year, uses, true, user.VoucherCode);
                if (!string.IsNullOrEmpty(user.VoucherCode))
                    await _voucherService.reduceCountVoucher(user.VoucherCode);
                await _userServiceRepository.CreateUserAsync(u);
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
                return Ok(new { code = ECodeResp.internalError, message = "Server có thể đang gặp vấn đề hày liên hệ người quản trị để được hỗ trợ" });
            }

            return Ok(new { objectId = u._id });
        }

        /// <summary>
        /// Lấy Thông tin User Đặt dich vu tại nhà theo ObjectId 
        /// </summary>
        /// <remarks>
        ///     Sample request:
        /// 
        ///         GET api/user/home_service/{objectId}
        /// 
        ///     Sample response:
        /// </remarks>
        /// <param name="objectId">_id cua object</param>
        /// <returns></returns>
        /// <response code="200">if null -> message: "Not Found"</response>
        /// <response code="400">if null -> message: "'objectId' is not a valid 24 digit hex string"</response>
        [HttpGet("home_service/{objectId}")]
        public async Task<ActionResult<Object>> GetUserServiceHomeByObjectIdAsync(
            [RegularExpression("^[0-9a-fA-F]{24}$",
                ErrorMessage = "'{0}' is not a valid 24 digit hex string")] string objectId)
        {
            var user = await _userServiceRepository.GetUserByObjectIdAsync(objectId);
            if (user is null)
                return StatusCode(200, new { code = ECodeResp.NotFound, message = "không tìm thấy thông tin user" });

            return _mapper.Map<UserServiceOrderDto>(user);
        }

        /// <summary>
        /// Lấy Thông tin User Đặt lịch theo ObjectId 
        /// </summary>
        /// <remarks>
        ///     Sample request:
        /// 
        ///         GET api/user/{objectId}
        /// 
        ///     Sample response:
        /// </remarks>
        /// <param name="objectId">_id cua object</param>
        /// <returns></returns>
        /// <response code="200">if null -> message: "Not Found"</response>
        /// <response code="400">if null -> message: "'objectId' is not a valid 24 digit hex string"</response>
        [HttpGet("{objectId}")]
        public async Task<ActionResult<Object>> GetUserByObjectIdAsync(
            [RegularExpression("^[0-9a-fA-F]{24}$",
                ErrorMessage = "'{0}' is not a valid 24 digit hex string")] string objectId)
        {
            var user = await _userRepository.GetUserByObjectIdAsync(objectId);
            if (user is null)
                return StatusCode(200, new { code = ECodeResp.NotFound, message = "không tìm thấy thông tin user" });

            return _mapper.Map<UserScheduleDto>(user);
        }

        /// <summary>
        /// Cập nhật Phí Dịch Vụ
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/user/costs_service/{CostsService}
        /// 
        /// </remarks>
        /// <param name="CostsService">Chi phí dịch vụ đăng kiểm</param>
        /// <returns>status code 200 no content</returns>
        /// <response code="200">ok</response>
        [HttpPut("costs_service/{CostsService}")]
        public ActionResult UpdateCostsService(int CostsService = 1000000)
        {
            try
            {
                if (CostsService < 0)
                    return Ok(new { code = ECodeResp.Info, message = "CostsService phải lớn hơn 0" });
                _logger.LogInformation($"CostService Đã được cập nhật từ giá ban đầu là: {_userService.CostsServicePublic} => {CostsService} VND");
                _userService.UpdateCostsService(CostsService);
            }
            catch (System.Exception e)
            {
                _logger.LogError($" {e.Message} {e.StackTrace}");
            }
            return Ok();
        }


        /// <summary>
        /// mã hóa id user 
        /// </summary>
        /// <remarks>
        ///     Sample request:
        /// 
        ///         POST api/user/encode_iduser
        /// 
        ///     Sample response:
        /// </remarks>
        /// <param name="objectId">_id cua object</param>
        /// <returns></returns>
        /// <response code="200">ObjectId: `24 digit hex string` || if null -> message: "Not Found"</response>
        /// <response code="400">if null -> message: "'objectId' is not a valid 24 digit hex string"</response>
        [HttpGet("encode_iduser/{objectId}")]
        public async Task<Object> enCodeIdUser(
            [RegularExpression("^[0-9a-fA-F]{24}$",
                ErrorMessage = "'{0}' is not a valid 24 digit hex string")] string objectId)
        {
            var cryptor = new XXTeaCryptor();
            var user = await _userRepository.GetUserByObjectIdAsync(objectId);
            if (user is null)
                return StatusCode(200, new { mcode = ECodeResp.NotFound, essage = "không tìm thấy thông tin user" });

            var encryptedData = cryptor.EncryptString(user._id, key); // Encrypt
            string hex = BitConverter.ToString(encryptedData).Replace("-", string.Empty);
            return Ok(new { code = hex });
        }

        /// <summary>
        /// Lấy Thông tin User Đặt lịch theo mã encode 
        /// </summary>
        /// <remarks>   
        ///     Sample request:
        /// 
        ///         POST api/user/infoUser/{encode}
        /// 
        ///     Sample response:
        /// </remarks>
        /// <param name="encode">chuỗi HEXA</param>
        /// <returns></returns>
        /// <response code="200">ObjectId: `24 digit hex string` || if null -> message: "Not Found"</response>
        /// <response code="400">if null -> message: "'objectId' is not a valid 24 digit hex string"</response>
        [HttpGet("infoUser")]
        public async Task<ActionResult<Object>> GetUserByEnCodeAsync([Required] string encode)
        {
            var cryptor = new XXTeaCryptor();
            UserSchedule user = null;
            try
            {
                var decryptedData = cryptor.DecryptString(encode.HexaToByteArray(), key); // Decrypt            
                user = await _userRepository.GetUserByObjectIdAsync(decryptedData);
            }
            catch (System.Exception)
            {
                return StatusCode(200, new { code = ECodeResp.NotFound, message = "Không tìm thấy thông tin người dùng" });
            }

            if (user is null) return StatusCode(200, new { code = ECodeResp.NotFound, message = "Không tìm thấy thông tin người dùng" });

            return _mapper.Map<UserScheduleDto>(user);
        }
    }
}