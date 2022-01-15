using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using  DK_API .Entities;
using  DK_API .Repository.InterfaceRepo;

namespace  DK_API .Repository
{
    public class MongodbStationDataRepo : IStationRepository
    {
        public static List<Station> DataStation;
        private const string databaseName = "Dk_db";
        private const string collectionName = "station_data";

        private readonly IMongoCollection<Station> StationsCollection;

        private readonly FilterDefinitionBuilder<Station> filterBuilder = Builders<Station>.Filter;

        public MongodbStationDataRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            StationsCollection = database.GetCollection<Station>(collectionName);

            // Get data
            DataStation = StationsCollection.Find(new BsonDocument()).ToList();
        }

        public async Task<IEnumerable<Station>> GetStationsAsync()
        {
            return await StationsCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<Station> GetStationAsync(int stationId)
        {
            var filter = filterBuilder.Eq(station => station.StationId, stationId);
            return await StationsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Station>> GetStationsByCityIdAsync(int cityId)
        {
            var filter = filterBuilder.Eq(station => station.CityId, cityId);
            return await StationsCollection.Find(filter).ToListAsync();
        }

        public async Task update(Station station){
            await StationsCollection.ReplaceOneAsync(
                filterBuilder.Eq(
                    exsta => exsta.StationId, station.StationId), station);
        }
    }
}