using System.Collections.Generic;
using DK_API.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_API.Entities
{       
    public class StationSlotData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string set { get; set; }
        [BsonElement("Station_Id")]
        public int stationId { get; set; }
        [BsonElement("slot_data")]
        public List<SlotData> slotData { get; set; }
        [BsonElement("update_time")]
        public string updateTime { get; set; }
    }
}