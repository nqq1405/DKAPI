using System;
using System.ComponentModel.DataAnnotations;

namespace DK_API.Dtos.ScheduleUser
{
    /// <summary>
    /// Thông tin user đặt lịch
    /// </summary>
    public class UserScheduleCreate
    {
        /// <summary>
        /// Tên đầy đủ người đặt lịch
        /// </summary>
        /// <value>string</value>
        /// <example>Nguyễn Văn A</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        [DataType(DataType.Text, ErrorMessage = "{0} không hợp lệ")]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\s]+$", ErrorMessage = "Name Invalid format")]
        public string Name { get; set; }

        /// <summary>
        /// Số điện thoại người đặt lịch
        /// </summary>
        /// <value>string</value>
        /// <example>0912393883</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8,10})\b", ErrorMessage = " format phone number range(8-12) ")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// biển số xe ô tô định 
        /// </summary>
        /// <value>string</value>
        /// <example>30A-246.56</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        [RegularExpression(@"((\d{2})[A-Z])(\-| |)(\d{4}([A-Z]|)|(\d{3}\.\d{2}))", ErrorMessage = "Invalid LicensePlates format ex: 30A-246.56")]
        public string LicensePlates { get; set; }

        /// <summary>
        /// mã thành phố trong danh sách thành phố (1-63)
        /// </summary>
        /// <value>int</value>
        /// <example>1</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public int CityId { get; set; }

        /// <summary>
        /// mã trạm trong danh sách trạm đăng kiểm
        /// </summary>
        /// <value>int</value>
        /// <example>1</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public int StationId { get; set; }

        /// <summary>
        /// Mã Phương tiện Đăng kiểm (1-11)
        /// </summary>
        /// <value>int</value>
        /// <example>1</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public int vehiclePKDId { get; set; }

        /// <summary>
        /// Mã Phương tiện đường bộ (12-19)
        /// </summary>
        /// <value>int</value>
        /// <example>12</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public int vehiclePDBId { get; set; }

        /// <summary>
        /// Hãng xe
        /// </summary>
        /// <value>string</value>
        /// <example>Toyota</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public string CarCompany { get; set; }

        /// <summary>
        /// Năm sản xuất xe (25 năm gần nhất)
        /// </summary>
        /// <value>int</value>
        /// <example>2020</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public int YearofManufacture { get; set; }

        /// <summary>
        /// thuộc sở hữu (Cá nhân:true || Doanh Nghiêp:false)
        /// </summary>
        /// <value>bool</value>
        /// <example>true</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public bool IsOwner { get; set; }

        /// <summary>
        /// Mục đích sử dụng (Kinh doanh:true || không kinh doanh:false)
        /// </summary>
        /// <value>bool</value>
        /// <example>true</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public bool Uses { get; set; }

        /// <summary>
        /// Sử dụng dịch vụ (có sử dụng:true || không sử dụng:false)
        /// </summary>
        /// <value>bool</value>
        /// <example>true</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public bool UseService { get; set; }

        /// <summary>
        /// Mã voucher cua người dùng
        /// </summary>
        /// <value>string</value>
        /// <example></example>
        public string VoucherCode { get; set; }

        /// <summary>
        /// Ngày đặt (format (yyyy-MM-DDT00:00:00.000000))
        /// </summary>
        /// <value>DateTime</value>
        /// <example>12-25-2021</example>
        [DataType(DataType.Date, ErrorMessage = "{0} ??")]
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public DateTime Schedule { get; set; }

        /// <summary>
        /// Khung giờ đặt (các string có sẵn trong lần lấy slot từ API /Time)
        /// </summary>
        /// <value>string</value>
        /// <example>08:00</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public string TimeSlot { get; set; }
    }
}