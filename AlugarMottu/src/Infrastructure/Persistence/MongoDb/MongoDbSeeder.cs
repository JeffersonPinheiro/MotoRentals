using MongoDB.Driver;
using MotorcycleRentals.src.Domain.Entities;

namespace MotorcycleRentals.src.Infrastructure.Persistence.MongoDb
{
    public static class MongoDbSeeder
    {
        public static async Task SeedAsync(MongoDbContext context)
        {
            // Motorcycle
            if ((await context.Motorcycles.CountDocumentsAsync(Builders<Motorcycle>.Filter.Empty)) == 0)
            {
                var motorcycles = new List<Motorcycle>
                {
                    new Motorcycle { Id = Guid.NewGuid(), Plate = "ABC1234", Model = "Honda CG 160", Year = 2023 },
                    new Motorcycle { Id = Guid.NewGuid(), Plate = "XYZ5678", Model = "Yamaha Factor", Year = 2024 },
                };
                await context.Motorcycles.InsertManyAsync(motorcycles);
            }

            // DeliveryMan
            if ((await context.DeliveryMen.CountDocumentsAsync(Builders<DeliveryMan>.Filter.Empty)) == 0)
            {
                var deliveryMen = new List<DeliveryMan>
                {
                    new DeliveryMan { Id = Guid.NewGuid(), Name = "João Silva", CnhNumber = "1234567890" },
                    new DeliveryMan { Id = Guid.NewGuid(), Name = "Maria Souza", CnhNumber = "0987654321" },
                };
                await context.DeliveryMen.InsertManyAsync(deliveryMen);
            }

            // Rental
            if ((await context.Rentals.CountDocumentsAsync(Builders<Rental>.Filter.Empty)) == 0)
            {
                var motorcycles = await context.Motorcycles.Find(Builders<Motorcycle>.Filter.Empty).ToListAsync();
                var deliveryMen = await context.DeliveryMen.Find(Builders<DeliveryMan>.Filter.Empty).ToListAsync();

                if (motorcycles.Count > 0 && deliveryMen.Count > 0)
                {
                    var rentals = new List<Rental>
                    {
                        new Rental
                        {
                            Id = Guid.NewGuid(),
                            MotorcycleId = motorcycles[0].Id,
                            DeliveryManId = deliveryMen[0].Id,
                            StartDate = DateTime.UtcNow.AddDays(-10),
                            EndDate = null
                        },
                        new Rental
                        {
                            Id = Guid.NewGuid(),
                            MotorcycleId = motorcycles[1].Id,
                            DeliveryManId = deliveryMen[1].Id,
                            StartDate = DateTime.UtcNow.AddDays(-5),
                            EndDate = DateTime.UtcNow.AddDays(-1)
                        }
                    };
                    await context.Rentals.InsertManyAsync(rentals);
                }
            }
        }
    }
}
