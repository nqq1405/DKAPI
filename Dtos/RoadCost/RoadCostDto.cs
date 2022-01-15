using System.ComponentModel.DataAnnotations;

namespace  DK_API .Dtos.RoadCost
{
    public record RoadCostDto
    {
        [Range(12, 19)]
        public int VehicleId { get; set; }
        public int PriceDB { get; set; }
    }
}