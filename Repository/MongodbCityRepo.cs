using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DK_API.Entities;
using DK_API.Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DK_API.Repository
{
    public class MongodbCityRepo : ICityRepository
    {
        public static List<City> CityData;
        private const string databaseName = "Dk_db";
        private const string collectionName = "city_data";

        private readonly FilterDefinitionBuilder<City> filterBuilder = Builders<City>.Filter;

        private readonly IMongoCollection<City> citysCollection;

        public MongodbCityRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            citysCollection = database.GetCollection<City>(collectionName);
            // Get Data
            CityData = citysCollection.Find(new BsonDocument()).ToList();
        }

        public async Task CreateCityAsync(City city)
        {
            await citysCollection.InsertOneAsync(city);
        }

        public async Task DeleteCityAsync(int cityId)
        {
            var filter = filterBuilder.Eq(city => city.CityId, cityId);
            await citysCollection.DeleteOneAsync(filter);
        }

        public async Task<City> GetCityAsync(int cityId)
        {
            var filter = filterBuilder.Eq(city => city.CityId, cityId);
            return await citysCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<City>> GetCitysAsync()
        {
            return await citysCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateCityAsync(City city)
        {
            var filter = filterBuilder.Eq(existingCity => existingCity.CityId, city.CityId);
            await citysCollection.ReplaceOneAsync(filter, city);
        }

        public async Task<City> GetCityByNameLowerAsync(string NameLower)
        {
            var filter = filterBuilder.Eq(city => city.NameLower, NameLower);
            return await citysCollection.Find(filter)?.SingleOrDefaultAsync();
        }
    }
}