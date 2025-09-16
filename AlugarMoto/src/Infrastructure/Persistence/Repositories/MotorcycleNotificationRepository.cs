using MongoDB.Bson;
using MongoDB.Driver;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.MongoDb;

namespace MotorcycleRentals.src.Infrastructure.Persistence.Repositories
{
    public class MotorcycleNotificationRepository : IMotorcycleNotificationRepository
    {
        private readonly IMongoCollection<MotorcycleNotification> _collection;

        public MotorcycleNotificationRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<MotorcycleNotification>("MotorcycleNotifications");
        }

        public async Task InsertNotificationAsync(MotorcycleNotification notification)
        {
            await _collection.InsertOneAsync(notification);
        }

        public async Task<IEnumerable<MotorcycleNotification>> GetNotificationsAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<MotorcycleNotification>> GetNotificationsByYearAsync(int year)
        {
            var filter = Builders<MotorcycleNotification>.Filter.Eq(x => x.Year, year);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<MotorcycleNotification>> GetNotificationsByPlateAsync(string plate)
        {
            var filter = Builders<MotorcycleNotification>.Filter.Regex(x => x.Plate, new MongoDB.Bson.BsonRegularExpression(plate, "i"));
            return await _collection.Find(filter).ToListAsync();
        }
    }
}
