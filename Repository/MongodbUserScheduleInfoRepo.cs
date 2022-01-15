using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace  DK_API .Repository
{
    public class MongodbUserScheduleInfoRepo : IScheUserRepository
    {
        private const string databaseName = "Dk_db";
        private const string collectionName = "user_schedule_info";
        private readonly IMongoCollection<UserSchedule> SchedulingUserCollection;
        private readonly FilterDefinitionBuilder<UserSchedule> filterBuilder = Builders<UserSchedule>.Filter;
        public MongodbUserScheduleInfoRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            SchedulingUserCollection = database.GetCollection<UserSchedule>(collectionName);
        }
        public async Task CreateUserAsync(UserSchedule user)
        {
            await SchedulingUserCollection.InsertOneAsync(user);
        }
        public async Task DeleteScheduleUserByObjectIdAsync(string _ObjectId)
        {
            var existingUser = filterBuilder.Eq(user => user._id, _ObjectId);
            await SchedulingUserCollection.DeleteOneAsync(existingUser);
        }
        public async Task<IEnumerable<UserSchedule>> GetAllUsersAsync()
        {
            return await SchedulingUserCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<IEnumerable<UserSchedule>> GetUsersByCityIdAsync(int cityId)
        {
            var filter = filterBuilder.Eq(user => user.CityId, cityId);
            return await SchedulingUserCollection.Find(filter).ToListAsync();
        }
        public async Task<UserSchedule> GetUserByObjectIdAsync(string _ObjectId)
        {
            var filter = filterBuilder.Eq(user => user._id, _ObjectId);
            return await SchedulingUserCollection.Find(filter).SingleOrDefaultAsync();
        }
        public async Task UpdateScheduleUserAsync(UserSchedule user)
        {
            var existingUser = filterBuilder.Eq(user => user._id, user._id);
            await SchedulingUserCollection.ReplaceOneAsync(existingUser, user);
        }
    }
}