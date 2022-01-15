using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_API.Entities
{
    /// <summary>
    /// Entity Slot
    /// </summary>
    public class Slot
    {
        [BsonElement("time")]
        public string Time { get; set; }
        [BsonElement("slot")]
        public int slot { get; set; }
        [BsonElement("status")]
        public bool status { get; set; }
    }

    /// <summary>
    /// dữ liệu Slot
    /// </summary>
    public class SlotData : BaseEntity
    {

        [BsonElement("day_of_week")]
        public int dayofWeek { get; set; }

        [BsonElement("time")]
        public List<Slot> slotLists { get; set; }
        [BsonElement("status")]
        public bool status { get; set; }

    }
}