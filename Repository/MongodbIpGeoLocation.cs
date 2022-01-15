using System.Threading.Tasks;
using DK_API.Entities;
using MongoDB.Driver;

namespace DK_API.Repository
{
    public class MongodbIpGeoLocation
    {
        private const string databaseName = "Dk_db";
        private const string collectionName = "ip_Geolocation";
        private readonly FilterDefinitionBuilder<IpGeolocation> filterBuilder = Builders<IpGeolocation>.Filter;
        private readonly IMongoCollection<IpGeolocation> ipGeolocationsCollection;
        
        public MongodbIpGeoLocation(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            ipGeolocationsCollection = database.GetCollection<IpGeolocation>(collectionName);
        }
        public async Task CreateOneAsync(IpGeolocation ipGeolocation)
        {
            await ipGeolocationsCollection.InsertOneAsync(ipGeolocation);
        }
        public async Task<IpGeolocation> GetOneByIpv4Async(string ipv4)
        {
            var filter = filterBuilder.Eq(loc => loc.ip, ipv4);
            return await ipGeolocationsCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}