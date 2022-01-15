using System;
using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.RoadCost
{
    /// <summary>
    /// Update giá của phương tiện
    /// </summary>
    /// <value></value>
    public class RoadCostUpdate
    {
        /// <summary>
        /// Giá của phương tiện đường bộ
        /// </summary>
        /// <value>100000</value>
        [Required]
        public int PriceDB { get; set; }
    }
}