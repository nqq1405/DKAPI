using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_API.Entities
{
    public class IpGeolocation : BaseEntity
    {
        [BsonElement("ip_v4")]
        public string ip { get; set; }
        [BsonElement("host_name")]
        public string hostname { get; set; }
        [BsonElement("city_name")]
        public string city { get; set; }
        [BsonElement("city_id")]
        public int cityId { get; set; }
        [BsonElement("region")]
        public string region { get; set; }
        [BsonElement("country")]
        public string country { get; set; }
        [BsonElement("location")]
        public string loc { get; set; }
        [BsonElement("org_ISP")]
        public string org { get; set; }
        [BsonElement("postal")]
        public string postal { get; set; }
        [BsonElement("time_zone")]
        public string timezone { get; set; }

    }
}