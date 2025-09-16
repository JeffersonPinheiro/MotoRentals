using MongoDB.Driver;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.MongoDb;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public class RentalRepository : MongoRepository<Rental>, IRentalRepository
    {
        private readonly IMongoCollection<Rental> _rentals;

        public RentalRepository(MongoDbContext context)
            : base(context, nameof(MongoDbContext.Rentals))
        {
            _rentals = context.Rentals;
        }

        public async Task AddAsync(Rental rental)
        {
            await _rentals.InsertOneAsync(rental);
        }

        public async Task UpdateAsync(Rental rental)
        {
            var filter = Builders<Rental>.Filter.Eq(x => x.Id, rental.Id);
            await _rentals.ReplaceOneAsync(filter, rental);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<Rental>.Filter.Eq(x => x.Id, id);
            await _rentals.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Rental>> GetRentalsByDeliveryManAsync(Guid deliveryManId)
        {
            var filter = Builders<Rental>.Filter.Eq(x => x.DeliveryManId, deliveryManId);
            return await _rentals.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetRentalsByMotorcycleAsync(Guid motorcycleId)
        {
            var filter = Builders<Rental>.Filter.Eq(x => x.MotorcycleId, motorcycleId);
            return await _rentals.Find(filter).ToListAsync();
        }
    }
}
