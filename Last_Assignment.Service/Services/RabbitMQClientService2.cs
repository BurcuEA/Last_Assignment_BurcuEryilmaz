﻿using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Last_Assignment.Service.Services
{
    public class RabbitMQClientService2 // : IDisposable
    {
        //private readonly ConnectionFactory _connectionFactory;
        //private IConnection _connection;
        //private IModel _channel;

        //private readonly ILogger<RabbitMQClientService> _logger;

        //public static string ExchangeName = "ExcelDirectExchange";
        //public static string RoutingExcel = "excel-route-file";
        //public static string QueueName = "queue-excel-file";

        //public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        //{
        //    _connectionFactory = connectionFactory;
        //    _logger = logger;
        //}

        //public IModel Connect()
        //{
        //    _connection = _connectionFactory.CreateConnection();

        //    if (_channel is { IsOpen: true })
        //    {
        //        return _channel;
        //    }

        //    _channel = _connection.CreateModel();

        //    _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

        //    _channel.QueueDeclare(QueueName, true, false, false, null);

        //    _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingExcel);

        //    _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");

        //    return _channel;
        //}

        //public void Dispose()
        //{
        //    _channel?.Close();
        //    _channel?.Dispose();

        //    _connection?.Close();
        //    _connection?.Dispose();

        //    _logger.LogInformation("RabbitMQ ile bağlantı koptu...");
        //}
    }
}