namespace  DK_API .Dtos
{
    public record StationDto
    {
        public int CityId { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
        public string Address { get; set; }
    }
}