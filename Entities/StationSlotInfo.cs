using System;
using System.Collections.Generic;
using DK_API .Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace  DK_API .Entities
{
    public class StationSlotInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("date")]
        public string date { get; set; }
        [BsonElement("station_id")]
        public int stationId {get; set;}
        [BsonElement("slot_data")]
        public List<Slot> slotLists { get; set; }
    }
}