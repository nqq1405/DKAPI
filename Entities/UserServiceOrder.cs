using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_API.Entities
{
    public class UserServiceOrder : UserBaseEntity
    {
        [BsonElement("Car_Delivery_Address")]
        public string carDeliveryAddress { get; set; }
        [BsonElement("Date")]
        public string Date { get; set; }
        [BsonElement("Time")]
        public string Time { get; set; }
    }
}