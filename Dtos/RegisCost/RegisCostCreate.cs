namespace DK_API.Dtos.RegisCost
{
    /// <summary>
    /// Tạo mới phí Đăng kiểm
    /// </summary>
    /// <value></value>
    public class RegisCostCreate
    {
        public int VehicleId { get; set; }
        public int PriceKD { get; set; }
        public int PriceCert { get; set; }
    }
}