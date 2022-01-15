using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetVehiclesByVehicleTypeAsync(string vehicleType);
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    }
}