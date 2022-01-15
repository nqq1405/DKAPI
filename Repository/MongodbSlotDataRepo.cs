using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace  DK_API .Repository
{
    public class MongodbSlotDataRepo : ISlotDataRepository
    {
        public static List<Slot> DataSlot;
        private const string databaseName = "Dk_db";
        private const string collectionName = "slot_data2";
        private readonly FilterDefinitionBuilder<SlotData> filterBuilder = Builders<SlotData>.Filter;

        private readonly IMongoCollection<SlotData> timesCollection;
        public MongodbSlotDataRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            timesCollection = database.GetCollection<SlotData>(collectionName);
            
            // get data
            DataSlot = timesCollection.Find(new BsonDocument()).FirstOrDefault().slotLists;
        }
        public async Task<SlotData> GetTimeAsync(int dotw)
        {
            var filter = filterBuilder.Eq(timer => timer.dayofWeek, dotw);
            return await timesCollection.Find(filter).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<SlotData>> GetTimesAsync()
        {
            return await timesCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}