using DK_API.Repository;
using DK_API.Repository.InterfaceRepo;

namespace DK_API.Service
{
    public class StationService
    {
        public StationService(IStationRepository repository) { }
        public bool IsCityId(int CityId)
        {
            bool result = false;
            MongodbStationDataRepo.DataStation.ForEach(station =>
            {
                if (station.CityId == CityId)
                    result = true;
            });
            return result;
        }

        public bool IsStationId(int stationId)
        {
            bool result = false;
            bool a = MongodbStationDataRepo.DataStation.Exists(s => s.StationId == stationId);
            if (a) result = true;
            return result;
        }
    }
}