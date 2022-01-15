using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace  DK_API .Entities
{
    /// <summary>
    /// Phí đường bộ
    /// </summary>
    /// <value></value>
    public class RoadCost : BaseEntity
    {
        public int VehicleId { get; set; }
        public int PriceDB { get; set; }
    }
}