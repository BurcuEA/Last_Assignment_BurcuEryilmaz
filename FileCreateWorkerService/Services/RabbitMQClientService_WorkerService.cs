using RabbitMQ.Client;

namespace FileCreateWorkerService.Services
{

    //public class RabbitMQClientService_WorkerService : IDisposable
    //{
    //    private readonly ConnectionFactory _connectionFactory;
    //    private IConnection _connection;
    //    private IModel _channel;

    //    private readonly ILogger<RabbitMQClientService_WorkerService> _logger;

    //    public static string ExchangeName = "ExcelDirectExchange";
    //    public static string RoutingExcel = "excel-route-file";
    //    public static string QueueName = "queue-excel-file";

    //    public RabbitMQClientService_WorkerService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService_WorkerService> logger)
    //    {
    //        _connectionFactory = connectionFactory;
    //        _logger = logger;
    //    }

    //    public IModel Connect()
    //    {
    //        _connection = _connectionFactory.CreateConnection();

    //        if (_channel is { IsOpen: true })
    //        {
    //            return _channel;
    //        }

    //        _channel = _connection.CreateModel();

    //        _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

    //        _channel.QueueDeclare(QueueName, true, false, false, null);

    //        _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingExcel);

    //        _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");

    //        return _channel;
    //    }

    //    public void Dispose()
    //    {
    //        _channel?.Close();
    //        _channel?.Dispose();

    //        _connection?.Close();
    //        _connection?.Dispose();

    //        _logger.LogInformation("RabbitMQ ile bağlantı koptu...");
    //    }
    //}










    #region Eski RabbitMQ Service
    //public class RabbitMQClientService_WorkerService : IDisposable
    //{
    //    private readonly ConnectionFactory _connectionFactory;
    //    private IConnection _connection;
    //    private IModel _channel;

    //    public static string QueueName = "queue-excel-file";

    //    private readonly ILogger<RabbitMQClientService_WorkerService> _logger;

    //    public RabbitMQClientService_WorkerService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService_WorkerService> logger)
    //    {
    //        _connectionFactory = connectionFactory;
    //        _logger = logger;
    //    }

    //    public IModel Connect()
    //    {
    //        _connection = _connectionFactory.CreateConnection();

    //        if (_channel is { IsOpen: true })
    //        {
    //            return _channel;
    //        }

    //        _channel = _connection.CreateModel();

    //        _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");

    //        return _channel;
    //    }

    //    public void Dispose()
    //    {
    //        _channel?.Close();
    //        _channel?.Dispose();

    //        _connection?.Close();
    //        _connection?.Dispose();

    //        _logger.LogInformation("RabbitMQ ile bağlantı koptu...");
    //    }
    //}
    #endregion

}

