using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  DK_API .Repository.InfRepository;

namespace  DK_API .Service
{
    public class YearService
    {
        public async Task<IEnumerable<int>> GetYearsAsync()
        {
            List<int> years = new List<int>();
            var year = DateTime.UtcNow.Year;
            int yearlast = year - 25;
            for (int i = year; i > yearlast; i--)
            {
                years.Add(i);
            }
            return await Task.FromResult(years);
        }

        public async Task<bool> Is25YearsNow (int year){
            List<int> years = (await GetYearsAsync()).ToList();
            bool result = true;
            years.ForEach(_year => {
                if(_year == year) result = false;
            });
            return result;
        }

    }
}