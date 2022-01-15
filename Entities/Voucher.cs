using System;
using System.Security.Cryptography.X509Certificates;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace  DK_API .Entities
{
    public record Voucher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("code")]
        public string Code { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("count")]
        public int Count { get; set; }
        [BsonElement("discount")]
        public int Discount { get; set; }
        [BsonElement("start_time")]
        public string StartTime { get; set; }
        [BsonElement("end_time")]
        public string EndTime { get; set; }
        [BsonElement("create_time")]
        public string CreatedDate { get; set; }
        [BsonElement("update_time")]
        public string UpdatedDate { get; set; }
        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; }
        [BsonElement("is_disabled")]
        public bool IsDisable { get; set; }
    }
}