using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace  DK_API .Repository
{
    public class MongodbStationSlotInfoRepo : IStationSlotInfoRepository
    {
        private const string databaseName = "Dk_db";
        private const string collectionName = "station_slot_date";

        private readonly FilterDefinitionBuilder<StationSlotInfo> filterBuilder = Builders<StationSlotInfo>.Filter;
        private readonly IMongoCollection<StationSlotInfo> ScheduleSlotsCollection;
        public MongodbStationSlotInfoRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            ScheduleSlotsCollection = database.GetCollection<StationSlotInfo>(collectionName);
        }
        public async Task CreateDateSlotAsync(StationSlotInfo slot)
        {
            await ScheduleSlotsCollection.InsertOneAsync(slot);
        }

        public async Task DeleteDateSlotAsync(string _id)
        {
            var filter = filterBuilder.Eq(s => s._id ,_id); 
            await ScheduleSlotsCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<StationSlotInfo>> GetDateSlotByDateAsync(string DateTime)
        {
            var filter = filterBuilder.Eq(s => s.date ,DateTime); 
            return await ScheduleSlotsCollection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<StationSlotInfo>> GetDateSlotsAsync()
        {
            return await ScheduleSlotsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateDateSlotAsync(StationSlotInfo existingSlot)
        {
            var filter = filterBuilder.Eq(existingSlot => existingSlot._id, existingSlot._id);
            await ScheduleSlotsCollection.ReplaceOneAsync(filter, existingSlot);
        }
    }
}