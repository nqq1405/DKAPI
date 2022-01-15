using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface IVoucherRepository
    {
        Task CreateVoucherAsync(Voucher voucher);
        Task<Voucher> GetVoucherAsync(string ObjectId);
        Task<IEnumerable<Voucher>> GetVouchersAsync();
        Task UpdateVoucherAsync(Voucher voucher);
        Task SoftDeleteVoucherAsync(string ObjectId);
        Task HardDeleteVoucherAsync(string ObjectId);
    }
}