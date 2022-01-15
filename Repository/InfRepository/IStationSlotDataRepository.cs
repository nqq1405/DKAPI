using System.Collections.Generic;
using System.Threading.Tasks;
using DK_API .Entities;

namespace DK_API .Repository.InfRepository
{
    public interface IStationSlotDataRepository
    {
        Task CreateManyStationSlotDataAsync(List<StationSlotData> stationSlotDatas);
        Task CreateStationSlotDataAsync(StationSlotData slot);
        Task<IEnumerable<StationSlotData>> GetStationSlotDatasAsync();
        Task<StationSlotData> GetStationSlotDataAsync(string _id);
        Task<StationSlotData> GetStationSlotDataByStationIdAsync(int stationId);
        Task UpdateStationSlotData(StationSlotData existingSlot);
        Task DeleteStationSlotData(string _id);
    }
}