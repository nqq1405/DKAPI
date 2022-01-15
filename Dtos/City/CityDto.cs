using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos
{
    public record CityDto
    {
        [Range(1,63)]
        public int CityId { get; set; }
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}