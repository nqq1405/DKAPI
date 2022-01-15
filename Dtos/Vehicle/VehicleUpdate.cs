using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.Vehicle
{
    public record VehicleUpdate
    {
        [Required]
        public bool TypeVehicle { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string NameVehicle { get; set; }
    }
}