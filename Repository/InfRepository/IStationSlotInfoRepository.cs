using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface IStationSlotInfoRepository
    {
        Task<IEnumerable<StationSlotInfo>> GetDateSlotsAsync();

        Task<IEnumerable<StationSlotInfo>> GetDateSlotByDateAsync(string DateTime);

        Task CreateDateSlotAsync(StationSlotInfo slot);

        Task UpdateDateSlotAsync(StationSlotInfo existingSlot);

        Task DeleteDateSlotAsync(string _id);
    }
}