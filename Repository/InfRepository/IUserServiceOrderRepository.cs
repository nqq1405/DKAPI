using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;

namespace  DK_API .Repository.InfRepository
{
    public interface IUserServiceOrderRepository
    {
        Task CreateUserAsync(UserServiceOrder user);
        Task<IEnumerable<UserServiceOrder>> GetAllUsersAsync();
        Task<UserServiceOrder> GetUserByObjectIdAsync(string _ObjectId);
        Task UpdateScheduleUserAsync(UserServiceOrder user);
        Task DeleteScheduleUserByObjectIdAsync(string _ObjectId);
        Task<IEnumerable<UserServiceOrder>> GetUsersByCityIdAsync(int cityId);
    }
}