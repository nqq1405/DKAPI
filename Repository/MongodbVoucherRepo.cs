using System.Collections.Generic;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository.InfRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace  DK_API .Repository
{
    public class MongodbVoucherRepo : IVoucherRepository
    {
        public static List<Voucher> DataVoucher;
        private const string databaseName = "Dk_db";
        private const string collectionName = "voucher";
        private readonly IMongoCollection<Voucher> voucherCollection;
        private readonly FilterDefinitionBuilder<Voucher> filterBuilder = Builders<Voucher>.Filter;
        public MongodbVoucherRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            voucherCollection = database.GetCollection<Voucher>(collectionName);
            DataVoucher = voucherCollection.Find(new BsonDocument()).ToList();
        }

        public async Task CreateVoucherAsync(Voucher voucher)
        {
            await voucherCollection.InsertOneAsync(voucher);
        }

        public async Task<Voucher> GetVoucherAsync(string ObjectId)
        {
            var filter = filterBuilder.Eq(voucher => voucher._id, ObjectId);
            return await voucherCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Voucher>> GetVouchersAsync()
        {
            return await voucherCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            var existingVoucher = filterBuilder.Eq(voucher => voucher._id, voucher._id);
            await voucherCollection.ReplaceOneAsync(existingVoucher, voucher);
        }

        public async Task SoftDeleteVoucherAsync(string ObjectId)
        {
            var existingVoucher = filterBuilder.Eq(voucher => voucher._id, ObjectId);
            var voucher = await voucherCollection.Find(existingVoucher).SingleOrDefaultAsync();
            if (voucher != null) voucher.IsDeleted = false;
            await voucherCollection.ReplaceOneAsync(existingVoucher, voucher);
        }

        public async Task HardDeleteVoucherAsync(string ObjectId)
        {
            var existingVoucher = filterBuilder.Eq(voucher => voucher._id, ObjectId);
            await voucherCollection.DeleteOneAsync(existingVoucher);
        }
    }
}