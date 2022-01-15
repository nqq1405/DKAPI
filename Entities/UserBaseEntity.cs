using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_API.Entities
{
    public class UserBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("Full_Name")]
        public string Name { get; set; }
        [BsonElement("Phone_Number")]
        public string PhoneNumber { get; set; }
        [BsonElement("License_Plates")]
        public string LicensePlates { get; set; }
        [BsonElement("City_Id")]
        public int CityId { get; set; }
        [BsonElement("vehiclePKD_Id")]
        public int vehiclePKDId { get; set; }
        [BsonElement("vehiclePDB_Id")]
        public int vehiclePDBId { get; set; }
        public string CarCompany { get; set; }
        [BsonElement("Year_of_Manufacture")]
        public int YearOfManufacture { get; set; }
        [BsonElement("Is_Owner")]
        public bool IsOwner { get; set; }
        [BsonElement("Is_Uses")]
        public bool Uses { get; set; }
        [BsonElement("Voucher_Code")]
        public string VoucherCode { get; set; }
        [BsonElement("Info_Costs")]
        public InfoCosts infocosts { get; set; }
        [BsonElement("Create_Time")]
        public string CreatedDate { get; set; }
    }
}