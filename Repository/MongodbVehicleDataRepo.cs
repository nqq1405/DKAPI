using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace  DK_API .Repository
{
    public class MongodbVehicleDataRepo : IVehicleRepository
    {
        public static List<Vehicle> DataVehicle;
        private const string databaseName = "Dk_db";
        private const string collectionName = "vehicle_data";
        private readonly IMongoCollection<Vehicle> vehiclesCollection;
        private readonly FilterDefinitionBuilder<Vehicle> filterBuilder = Builders<Vehicle>.Filter;
        public MongodbVehicleDataRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            vehiclesCollection = database.GetCollection<Vehicle>(collectionName);
            DataVehicle = vehiclesCollection.Find(new BsonDocument()).ToList();
        }
        public async Task<IEnumerable<Vehicle>> GetVehiclesByVehicleTypeAsync(string vehicleType)
        {
            var filter = filterBuilder.Eq(vehicle => vehicle.TypeVehicle, vehicleType);
            return await vehiclesCollection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await vehiclesCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}