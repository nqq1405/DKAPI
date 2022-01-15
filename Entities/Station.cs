using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DK_API.Entities
{
    public class Station : BaseEntity
    {
        public int StationId { get; set; }
        public int CityId { get; set; }
        public string StationName { get; set; }

    }
}