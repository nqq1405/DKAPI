using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface IScheUserRepository
    {
        Task CreateUserAsync(UserSchedule user);
        Task<IEnumerable<UserSchedule>> GetAllUsersAsync();
        Task<UserSchedule> GetUserByObjectIdAsync(string _ObjectId);
        Task UpdateScheduleUserAsync(UserSchedule user);
        Task DeleteScheduleUserByObjectIdAsync(string _ObjectId);
        Task<IEnumerable<UserSchedule>> GetUsersByCityIdAsync(int cityId);
    }
}