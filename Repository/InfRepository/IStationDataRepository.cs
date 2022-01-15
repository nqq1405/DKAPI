using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InterfaceRepo
{
    public interface IStationRepository
    {
        Task<IEnumerable<Station>> GetStationsAsync();
        Task<IEnumerable<Station>> GetStationsByCityIdAsync(int cityId);
        Task<Station> GetStationAsync(int stationId);
        Task update(Station station);
    }
}