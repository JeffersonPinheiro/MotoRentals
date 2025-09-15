using MongoDB.Driver;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.MongoDb;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public class DeliveryManRepository : MongoRepository<DeliveryMan>, IDeliveryManRepository
    {
        private readonly IMongoCollection<DeliveryMan> _deliveryMen;

        public DeliveryManRepository(MongoDbContext context)
            : base(context, nameof(MongoDbContext.DeliveryMen))
        {
            _deliveryMen = context.DeliveryMen;
        }

        public async Task AddAsync(DeliveryMan deliveryMan)
        {
            await _deliveryMen.InsertOneAsync(deliveryMan);
        }

        public async Task UpdateAsync(DeliveryMan deliveryMan)
        {
            var filter = Builders<DeliveryMan>.Filter.Eq(x => x.Id, deliveryMan.Id);
            await _deliveryMen.ReplaceOneAsync(filter, deliveryMan);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<DeliveryMan>.Filter.Eq(x => x.Id, id);
            await _deliveryMen.DeleteOneAsync(filter);
        }

        public async Task<DeliveryMan> GetByCnpjAsync(string cnpj)
        {
            var filter = Builders<DeliveryMan>.Filter.Eq(x => x.Cnpj, cnpj);
            return await _deliveryMen.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DeliveryMan>> FilterByCnpjAsync(string cnpjPartial)
        {
            var filter = Builders<DeliveryMan>.Filter.Regex(x => x.Cnpj, new MongoDB.Bson.BsonRegularExpression(cnpjPartial, "i"));
            return await _deliveryMen.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<DeliveryMan>> FilterByCnhNumberAsync(string cnhPartial)
        {
            var filter = Builders<DeliveryMan>.Filter.Regex(x => x.CnhNumber, new MongoDB.Bson.BsonRegularExpression(cnhPartial, "i"));
            return await _deliveryMen.Find(filter).ToListAsync();
        }

        public async Task<DeliveryMan> GetByCnhNumberAsync(string cnhNumber)
        {
            var filter = Builders<DeliveryMan>.Filter.Eq(x => x.CnhNumber, cnhNumber);
            return await _deliveryMen.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> CnpjExistsAsync(string cnpj)
        {
            var filter = Builders<DeliveryMan>.Filter.Eq(x => x.Cnpj, cnpj);
            var count = await _deliveryMen.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<bool> CnhNumberExistsAsync(string cnhNumber)
        {
            var filter = Builders<DeliveryMan>.Filter.Eq(x => x.CnhNumber, cnhNumber);
            var count = await _deliveryMen.CountDocumentsAsync(filter);
            return count > 0;
        }
    }
}
