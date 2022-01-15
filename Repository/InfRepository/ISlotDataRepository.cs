using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface ISlotDataRepository
    {
        Task<IEnumerable<SlotData>> GetTimesAsync();
        Task<SlotData> GetTimeAsync(int d);
    }
}