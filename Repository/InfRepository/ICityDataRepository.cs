using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface ICityRepository
    {
        Task<City> GetCityAsync(int cityId);
        Task<City> GetCityByNameLowerAsync(string NameLower);
        Task<IEnumerable<City>> GetCitysAsync();
        Task CreateCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task DeleteCityAsync(int cityId);
    }
}