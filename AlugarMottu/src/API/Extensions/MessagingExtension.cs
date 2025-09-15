using MotorcycleRentals.src.Infrastructure.Messaging.RabbitMQ;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;

namespace MotorcycleRentals.src.API.Extensions
{
    public static class MessagingExtension
    {
        public static IServiceCollection AddRabbitMqMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var hostName = configuration.GetSection("RabbitMq:HostName").Value ?? "localhost";

            services.AddSingleton<MotorcycleCreatedProducer>(sp => new MotorcycleCreatedProducer(hostName));
            services.AddSingleton<MotorcycleCreatedConsumer>();

            services.AddScoped<IMotorcycleNotificationRepository, MotorcycleNotificationRepository>();
            return services;
        }
    }
}
