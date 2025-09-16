using MotorcycleRentals.src.Application.Services;

namespace MotorcycleRentals.src.API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<MotorcycleService>();
            services.AddScoped<DeliveryManService>();
            services.AddScoped<RentalService>();
            return services;
        }
    }
}
