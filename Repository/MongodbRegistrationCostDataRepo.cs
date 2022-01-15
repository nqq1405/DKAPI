using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace  DK_API .Repository
{
    public class MongodbRegistrationCostDataRepo : IPKDRepository
    {
        private const string databaseName = "Dk_db";
        private const string collectionName = "registration_cost_data";

        private readonly FilterDefinitionBuilder<RegisCost> filterBuilder = Builders<RegisCost>.Filter;

        private readonly IMongoCollection<RegisCost> pkdsCollection;
        public static List<RegisCost> DataPKD;

        public MongodbRegistrationCostDataRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            pkdsCollection = database.GetCollection<RegisCost>(collectionName);
            
            // get data
            DataPKD = pkdsCollection.Find(new BsonDocument()).ToList();
        }

        public async Task<RegisCost> GetPKDAsync(int vehicleId)
        {
            var filter = filterBuilder.Eq(pkd => pkd.VehicleId, vehicleId);
            return await pkdsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<RegisCost>> GetPKDsAsync()
        {
            return await pkdsCollection.Find(new BsonDocument()).ToListAsync();

        }

        public async Task UpdatePKDAsync(RegisCost pkdc)
        {
            var filter = filterBuilder.Eq(pkd => pkd.VehicleId, pkdc.VehicleId);
            DataPKD = pkdsCollection.Find(new BsonDocument()).ToList();
            await pkdsCollection.ReplaceOneAsync(filter, pkdc);
        }
    }
}