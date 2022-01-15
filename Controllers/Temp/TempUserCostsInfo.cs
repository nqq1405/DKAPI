using System.ComponentModel.DataAnnotations;

namespace DK_API.Controllers.Temp
{
    /// <summary>
    /// A Info client tracks a task
    /// </summary>
    public class TempUserCostsInfo
    {
        /// <summary>
        /// Id của loại xe đường bộ
        /// </summary>
        /// <value>(12,19)</value>
        /// <example>12</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        [Range(12, 19)]
        public int vehiclePdbId { get; set; }

        /// <summary>
        /// Id của loại xe đăng kiểm
        /// </summary>
        /// <value>(1,11)</value>
        /// <example>1</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        [Range(1, 11)]
        public int vehiclePkdId { get; set; }

        /// <summary>
        /// Năm sản xuất trong 25 nam
        /// </summary>
        /// <example>2020</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public int year { get; set; }

        /// <summary>
        /// Mục đích sử dụng
        /// </summary>
        /// <value>true or false</value>
        /// <example>true</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public bool uses { get; set; }

        /// <summary>
        /// Sử dụng dịch vụ
        /// </summary>
        /// <value>true or false</value>
        /// <example>true</example>
        [Required(ErrorMessage = "{0} cần thiết lập")]
        public bool service { get; set; }

        /// <summary>
        /// Mã Voucher
        /// </summary>
        /// <value>string</value>
        /// <example>SALE1212</example>
        public string Code { get; set; }
        /// <summary>
        /// Biển số xe
        /// </summary>
        /// <value>string</value>
        /// <example>30A-246.56</example>
        [Required]
        public string LicensePlates { get; set; }
    }
}