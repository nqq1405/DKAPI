using System.Collections.Generic;
using System.Threading.Tasks;
using DK_API.Entities;
using DK_API.Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DK_API.Repository
{
    public class MongodbUserServiceOrderRepo : IUserServiceOrderRepository
    {
        private const string databaseName = "Dk_db";
        private const string collectionName = "user_service_home";
        private readonly IMongoCollection<UserServiceOrder> UserServiceOrderCollection;
        private readonly FilterDefinitionBuilder<UserServiceOrder> filterBuilder = Builders<UserServiceOrder>.Filter;
        public MongodbUserServiceOrderRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            UserServiceOrderCollection = database.GetCollection<UserServiceOrder>(collectionName);
        }

        public async Task CreateUserAsync(UserServiceOrder user)
        {
            await UserServiceOrderCollection.InsertOneAsync(user);
        }

        public async Task DeleteScheduleUserByObjectIdAsync(string _ObjectId)
        {
            var existingUser = filterBuilder.Eq(user => user._id, _ObjectId);
            await UserServiceOrderCollection.DeleteOneAsync(existingUser);
        }

        public async Task<IEnumerable<UserServiceOrder>> GetAllUsersAsync()
        {
            return await UserServiceOrderCollection.Find(new BsonDocument()).ToListAsync();

        }

        public async Task<UserServiceOrder> GetUserByObjectIdAsync(string _ObjectId)
        {
            var filter = filterBuilder.Eq(user => user._id, _ObjectId);
            return await UserServiceOrderCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserServiceOrder>> GetUsersByCityIdAsync(int cityId)
        {
            var filter = filterBuilder.Eq(user => user.CityId, cityId);
            return await UserServiceOrderCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateScheduleUserAsync(UserServiceOrder user)
        {
            var existingUser = filterBuilder.Eq(user => user._id, user._id);
            await UserServiceOrderCollection.ReplaceOneAsync(existingUser, user);

        }
    }
}