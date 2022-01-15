using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_API.Entities
{
    public class InfoCosts
    {
        [BsonElement("Regis_Cost")]
        public int CostPkd { get; set; }
        [BsonElement("Cert_Cost")]
        public int CostCert { get; set; }
        [BsonElement("Road_Cost")]
        public int CostPdb { get; set; }
        [BsonElement("Service_Cost")]
        public int CostService { get; set; }
        [BsonElement("Cost_Total")]
        public int CostTotalTemp { get; set; }
    }
    public class UserSchedule : UserBaseEntity
    {
        [BsonElement("Station_Id")]
        public int StationId { get; set; }
        [BsonElement("Is_UseService")]
        public bool UseService { get; set; }
        [BsonElement("Schedule_Date")]
        public string Schedule { get; set; }
        [BsonElement("Time_Slot")]
        public string TimeSlot { get; set; }
    }
}