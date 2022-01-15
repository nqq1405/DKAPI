using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.RegisCost
{
    public record RegisCostDto
    {
        [Range(1, 11)]
        public int VehicleId { get; set; }
        public int PriceKD { get; set; }
        public int PriceCert { get; set; }
    }
}