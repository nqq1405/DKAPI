using System.Collections.Generic;
using System.Threading.Tasks;
using DK_API.Entities;
using DK_API.Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DK_API.Repository
{
    public class MongodbStationSlotDataRepo : IStationSlotDataRepository
    {
        private const string databaseName = "Dk_db";
        private const string collectionName = "station_slot";
        private readonly FilterDefinitionBuilder<StationSlotData> filterBuilder = Builders<StationSlotData>.Filter;
        private readonly IMongoCollection<StationSlotData> StationSlotDataCollection;

        public MongodbStationSlotDataRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            StationSlotDataCollection = database.GetCollection<StationSlotData>(collectionName);
        }

        public async Task CreateStationSlotDataAsync(StationSlotData slot)
        {
            await StationSlotDataCollection.InsertOneAsync(slot);
        }
        public async Task CreateManyStationSlotDataAsync(List<StationSlotData> stationSlotDatas)
        {
            await StationSlotDataCollection.InsertManyAsync(stationSlotDatas);
        }
        public async Task DeleteStationSlotData(string _id)
        {
            var existStationSlotData = filterBuilder.Eq(s => s.set, _id);
            await StationSlotDataCollection.DeleteOneAsync(existStationSlotData);
        }
        public async Task<StationSlotData> GetStationSlotDataAsync(string _id)
        {
            var existStationSlotData = filterBuilder.Eq(s => s.set, _id);
            return await StationSlotDataCollection.Find(existStationSlotData).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<StationSlotData>> GetStationSlotDatasAsync()
        {
            return await StationSlotDataCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task UpdateStationSlotData(StationSlotData existingSlot)
        {
            var existStationSlotData = filterBuilder.Eq(s => s.set, existingSlot.set);
            await StationSlotDataCollection.ReplaceOneAsync(existStationSlotData, existingSlot);
        }

        public async Task<StationSlotData> GetStationSlotDataByStationIdAsync(int stationId)
        {
            var existStationSlotData = filterBuilder.Eq(s => s.stationId, stationId);
            return await StationSlotDataCollection.Find(existStationSlotData).SingleOrDefaultAsync();
        }
    }
}
