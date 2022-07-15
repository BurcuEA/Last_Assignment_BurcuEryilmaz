using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace SharedLibrary.Services
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        private readonly ILogger<RabbitMQClientService> _logger;

        public static string ExchangeName = "ExcelMailDirectExchange";        
        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile bağlantı koptu...");
        }
    }
}

