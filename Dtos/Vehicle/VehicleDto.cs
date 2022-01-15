using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos
{
    public class VehicleDto
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string TypeVehicle { get; set; }
        public string NameVehicle { get; set; }
    }
}