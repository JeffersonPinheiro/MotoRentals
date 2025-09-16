using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;
using MotorcycleRentals.src.Domain.Entities;
using System.Text;
using Newtonsoft.Json;

namespace MotorcycleRentals.src.Infrastructure.Messaging.RabbitMQ
{
    public class MotorcycleCreatedConsumerService : BackgroundService
    {
        private readonly ILogger<MotorcycleCreatedConsumerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _queueName = "motorcycle_created";
        private readonly string _hostName;

        public MotorcycleCreatedConsumerService(ILogger<MotorcycleCreatedConsumerService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _hostName = configuration.GetSection("RabbitMq:HostName").Value ?? "localhost";
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            _logger.LogInformation("RabbitMQ Consumer started for queue: {QueueName}", _queueName);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var motorcycle = JsonConvert.DeserializeObject<Motorcycle>(message);

                    if (motorcycle != null && motorcycle.Year == 2024)
                    {
                        _logger.LogInformation("Received motorcycle of year 2024: {@Motorcycle}", motorcycle);

                        using var scope = _serviceProvider.CreateScope();
                        var notificationRepo = scope.ServiceProvider.GetRequiredService<IMotorcycleNotificationRepository>();

                        var notification = new MotorcycleNotification
                        {
                            Id = Guid.NewGuid(),
                            MotorcycleId = motorcycle.Id,
                            Plate = motorcycle.Plate,
                            Model = motorcycle.Model,
                            Year = motorcycle.Year,
                            NotifiedAt = DateTime.UtcNow
                        };

                        await notificationRepo.InsertNotificationAsync(notification);

                        _logger.LogInformation("Notification persisted for motorcycle: {Plate}", motorcycle.Plate);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing RabbitMQ message.");
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
