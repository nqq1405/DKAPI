namespace DK_API.Dtos.RoadCost
{
    /// <summary>
    /// Tạo mới phí đường bộ
    /// </summary>
    /// <value></value>
    public class RoadCostCreate
    {
        public int VehicleId { get; set; }
        public int PriceDB { get; set; }
    }
}