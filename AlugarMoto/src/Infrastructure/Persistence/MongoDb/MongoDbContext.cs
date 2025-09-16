using MongoDB.Bson;
using MongoDB.Driver;
using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.Infrastructure.Persistence.MongoDb
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDb:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDb:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Coleções principais
        public IMongoCollection<Motorcycle> Motorcycles =>
            _database.GetCollection<Motorcycle>("Motorcycles");

        public IMongoCollection<DeliveryMan> DeliveryMen =>
            _database.GetCollection<DeliveryMan>("DeliveryMen");

        public IMongoCollection<Rental> Rentals =>
            _database.GetCollection<Rental>("Rentals");

        public IMongoCollection<MotorcycleNotification> MotorcycleNotifications =>
            _database.GetCollection<MotorcycleNotification>("MotorcycleNotifications");

        // Método genérico para facilitar DI dos repositórios
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
