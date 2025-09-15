using System.Text;
using MongoDB.Bson.IO;
using MotorcycleRentals.src.Domain.Entities;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace MotorcycleRentals.src.Infrastructure.Messaging.RabbitMQ
{
    public class MotorcycleCreatedConsumer
    {
        private readonly string _hostName;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _queueName = "motorcycle_created";

        public MotorcycleCreatedConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _hostName = configuration["RabbitMq:HostName"];
            _serviceProvider = serviceProvider;
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motorcycle = JsonConvert.DeserializeObject<MotorcycleNotification>(message);

                if (motorcycle?.Year == 2024)
                {
                    // Cria um escopo novo a cada mensagem para resolver serviços Scoped
                    using var scope = _serviceProvider.CreateScope();
                    var repo = scope.ServiceProvider.GetRequiredService<IMotorcycleNotificationRepository>();

                    await repo.InsertNotificationAsync(motorcycle);
                }
            };

            channel.BasicConsume(
                queue: _queueName,
                autoAck: true,
                consumer: consumer
            );
        }
    }
}
