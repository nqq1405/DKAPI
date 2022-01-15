using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface IPKDRepository
    {
        Task<RegisCost> GetPKDAsync(int vehicleId);
        Task<IEnumerable<RegisCost>> GetPKDsAsync();
        Task UpdatePKDAsync(RegisCost pkdc);
    }
}