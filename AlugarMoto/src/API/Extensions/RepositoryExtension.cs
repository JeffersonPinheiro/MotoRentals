using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.MongoDb;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;

namespace MotorcycleRentals.src.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IMotorcycleNotificationRepository, MotorcycleNotificationRepository>();

            return services;
        }
    }
}
