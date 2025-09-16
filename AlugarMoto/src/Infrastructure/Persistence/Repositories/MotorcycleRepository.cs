using MongoDB.Driver;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.MongoDb;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public class MotorcycleRepository : MongoRepository<Motorcycle>, IMotorcycleRepository
    {
        private readonly IMongoCollection<Motorcycle> _motorcycles;
        private readonly IMongoCollection<Rental> _rentals;

        public MotorcycleRepository(MongoDbContext context)
            : base(context, nameof(MongoDbContext.Motorcycles))
        {
            _motorcycles = context.Motorcycles;
            _rentals = context.Rentals;
        }

        public async Task AddAsync(Motorcycle motorcycle)
        {
            await _motorcycles.InsertOneAsync(motorcycle);
        }

        public async Task UpdateAsync(Motorcycle motorcycle)
        {
            var filter = Builders<Motorcycle>.Filter.Eq(x => x.Id, motorcycle.Id);
            await _motorcycles.ReplaceOneAsync(filter, motorcycle);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<Motorcycle>.Filter.Eq(x => x.Id, id);
            await _motorcycles.DeleteOneAsync(filter);
        }

        public async Task<Motorcycle> GetByPlateAsync(string plate)
        {
            var filter = Builders<Motorcycle>.Filter.Eq(x => x.Plate, plate);
            return await _motorcycles.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Motorcycle>> GetByPlateFilterAsync(string plate)
        {
            var filter = Builders<Motorcycle>.Filter.Regex(x => x.Plate, new MongoDB.Bson.BsonRegularExpression(plate, "i"));
            return await _motorcycles.Find(filter).ToListAsync();
        }

        public async Task<bool> PlateExistsAsync(string plate)
        {
            var filter = Builders<Motorcycle>.Filter.Eq(x => x.Plate, plate);
            var count = await _motorcycles.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<bool> HasActiveRentalAsync(Guid motorcycleId)
        {
            var filter = Builders<Rental>.Filter.Eq(x => x.MotorcycleId, motorcycleId) &
                         Builders<Rental>.Filter.Eq(x => x.Status, Domain.Enums.RentalStatus.Active);
            var count = await _rentals.CountDocumentsAsync(filter);
            return count > 0;
        }
    }
}
