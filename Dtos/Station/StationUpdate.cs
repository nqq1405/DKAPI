using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.Station
{
    public record StationUpdate
    {
        [Required]
        [DataType(DataType.Text)]
        public string StationName { get; set; }
    }
}