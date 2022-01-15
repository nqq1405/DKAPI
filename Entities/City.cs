using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace  DK_API .Entities
{
    public class City : BaseEntity
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string NameLower { get; set; }
    }
}