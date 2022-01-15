using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface IPDBRepository
    {
        Task<RoadCost> GetPDBAsync(int PdbId);
        Task<IEnumerable<RoadCost>> GetPDBsAsync();
        Task UpdatePDBAsync(RoadCost pdbc);
    }
}