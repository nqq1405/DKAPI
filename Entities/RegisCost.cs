using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace  DK_API .Entities
{
    /// <summary>
    /// Phí Đăng kiểm
    /// </summary>
    /// <value></value>
    public class RegisCost : BaseEntity
    {
        public int VehicleId { get; set; }
        public int PriceKD { get; set; }
        public int PriceCert { get; set; }
    }
}