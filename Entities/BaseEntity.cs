using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace DK_API.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("is_deleted")]
        public bool IsDelete { get; set; } = false;
        [BsonElement("Create_Time")]
        public string CreatedDate { get; set; }
        [BsonElement("update_time")]
        public string UpdatedDate { get; set; }
    }
}