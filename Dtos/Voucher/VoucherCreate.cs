using System;
using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.Voucher
{
    /// <summary>
    /// Thông tin Voucher Create
    /// </summary>
    public class VoucherCreate
    {
        /// <summary>
        /// Mã Voucher
        /// </summary>
        /// <value>string</value>
        /// <example>SALE1212</example>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Mô tả mã giảm giá 
        /// </summary>
        /// <value>string</value>
        /// <example>Đại giảm giá tháng 12, kích thích tiêu dùng</example>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// số lượng mã giảm giá
        ///     note: count > 0
        /// </summary>
        /// <value>int</value>
        /// <example>100</example>
        [Required]
        public int Count { get; set; }
        /// <summary>
        /// Giá trị giảm giá
        ///     nếu type ở trên là:
        ///         + Phần trăm: 0.01 -> 1 (1% -> 100%)
        ///         + Giảm thẳng giá: min:10000 
        /// </summary>
        /// <value>double</value>
        /// <example>50000</example>
        [Required]
        public int Discount { get; set; }
        /// <summary>
        /// Thời gian bắt đầu (nên để thời gian tuyệt đối đến giây )
        ///     Định dạng khuyến nghị "yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss"
        ///     example -> 	2020-05-16T05:50:06
        /// </summary>
        /// <value>DateTime</value>
        /// <example>2021-12-13T00:00:00</example>
        [Required]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Thời gian Kết thúc (nên để thời gian tuyệt đối đến giây )
        ///     Định dạng khuyến nghị "yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss"
        ///     example -> 	2020-05-16T05:50:06
        /// - chú ý: thời gian kết thúc cần phải sớm hơn thời gian hiện tại và sớm hơn thời gian bắt đầu(StartTime) 
        /// </summary>
        /// <value>DateTime</value>
        /// <example>2021-12-30T00:00:00</example>
        [Required]
        public DateTime EndTime { get; set; }
        /// <summary>
        ///     Thuộc tính cho biết voucher này có được áp dụng luôn hay k
        ///     nếu muốn áp dụng luôn -> false  
        ///     nếu không muốn áp dụng luôn -> true  
        ///</summary>
        /// <value>bool</value>
        /// <example>false</example>
        public bool IsDisable { get; set; }
    }
}