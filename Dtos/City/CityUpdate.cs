using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.City
{
    public record CityUpdate
    {
        [Required]
        public string Name { get; set; }
    }
}