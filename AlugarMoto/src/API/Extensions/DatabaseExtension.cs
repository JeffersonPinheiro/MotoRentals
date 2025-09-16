using MotorcycleRentals.src.Infrastructure.Persistence.MongoDb;

namespace MotorcycleRentals.src.API.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new MongoDbSettings();
            configuration.GetSection("MongoDb").Bind(settings);
            services.AddSingleton(settings);
            services.AddSingleton<MongoDbContext>();
            return services;
        }
    }
}
