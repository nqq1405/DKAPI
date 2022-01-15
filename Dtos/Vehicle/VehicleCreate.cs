using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.Vehicle
{

    public record VehicleCreate
    {
        [Required (ErrorMessage="{0} cần thiết lập")]
        public bool TypeVehicle { get; set; }
        
        [Required (ErrorMessage="{0} cần thiết lập")]
        [DataType(DataType.Text)]
        public string NameVehicle { get; set; }
    }
}