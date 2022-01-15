using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace  DK_API .Repository
{
    public class MongodbRoadCostDataRepo : IPDBRepository
    {
        public static List<RoadCost> DataPDB; 
        private const string databaseName = "Dk_db";
        private const string collectionName = "road_cost_data";

        private readonly FilterDefinitionBuilder<RoadCost> filterBuilder = Builders<RoadCost>.Filter;

        private readonly IMongoCollection<RoadCost> pdbsCollection;

        public MongodbRoadCostDataRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            pdbsCollection = database.GetCollection<RoadCost>(collectionName);
        
            // get data
            DataPDB = pdbsCollection.Find(new BsonDocument()).ToList();
        }

        public async Task<RoadCost> GetPDBAsync(int VehicleId)
        {
            var filter = filterBuilder.Eq(pdb => pdb.VehicleId, VehicleId);
            return await pdbsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<RoadCost>> GetPDBsAsync()
        {
            return await pdbsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdatePDBAsync(RoadCost pdbc)
        {
            var filter = filterBuilder.Eq(pdb => pdb.VehicleId, pdbc.VehicleId);
            DataPDB = pdbsCollection.Find(new BsonDocument()).ToList();
            await pdbsCollection.ReplaceOneAsync(filter, pdbc);
        }
    }
}