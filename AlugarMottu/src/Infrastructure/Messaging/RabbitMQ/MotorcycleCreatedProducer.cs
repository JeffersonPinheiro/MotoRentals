using MotorcycleRentals.src.Domain.Entities;
using System.Text;
using RabbitMQ.Client;
namespace MotorcycleRentals.src.Infrastructure.Messaging.RabbitMQ
{
    public class MotorcycleCreatedProducer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "motorcycle_created";

        public MotorcycleCreatedProducer(string hostName)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(Motorcycle motorcycle)
        {
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(motorcycle);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: _queueName, body: body);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
