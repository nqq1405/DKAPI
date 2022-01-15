using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace  DK_API .Entities
{
    public class Vehicle : BaseEntity
    {
        public int VehicleId { get; set; }
        public string TypeVehicle { get; set; }
        public string NameVehicle { get; set; }
    }
}