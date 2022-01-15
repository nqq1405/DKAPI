using System;
using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.RegisCost
{
    public record RegisCostUpdate
    {
        /// <summary>
        /// Giá Kiểm định
        /// </summary>
        /// <value>int</value>
        /// <example>100000</example>
        [Required]
        public int PriceKD { get; set; }
        
        /// <summary>
        /// Giá Chứng Chỉ
        /// </summary>
        /// <value>int</value>
        /// <example>50000</example>
        [Required]
        public int PriceCert { get; set; }
    }
}