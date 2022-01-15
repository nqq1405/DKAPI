using  DK_API .Entities;

namespace  DK_API .Dtos.ScheduleUser
{
    public class UserScheduleDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string LicensePlates { get; set; }
        public int CityId { get; set; }
        public int StationId { get; set; }
        public string CarCompany { get; set; }
        public int YearOfManufacture { get; set; }
        public bool IsOwner { get; set; }
        public bool Uses { get; set; }
        public bool UseService { get; set; }
        public string VoucherCode { get; set; }
        public InfoCosts infocosts { get; set; }
        public string Schedule { get; set; }
        public string TimeSlot { get; set; }
        public string CreatedDate { get; set; }
    }
}