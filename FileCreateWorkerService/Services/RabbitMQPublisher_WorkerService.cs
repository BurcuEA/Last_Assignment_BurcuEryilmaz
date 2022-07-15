using FileCreateWorkerService.Services;
using RabbitMQ.Client;
using SharedLibrary;
using SharedLibrary.Dtos;
using System.Text;
using System.Text.Json;

namespace Last_Assignment.Service.Services
{
    //public class RabbitMQPublisher_WorkerService
    //{
    //    private readonly RabbitMQClientService_WorkerService _rabbitMQClientService;

    //    public RabbitMQPublisher_WorkerService(RabbitMQClientService_WorkerService rabbitMQClientService)
    //    {
    //        _rabbitMQClientService = rabbitMQClientService;
    //    }

    //    public void Publish(CreateExcelMessage createExcelMessage)
    //    {
    //        var channel = _rabbitMQClientService.Connect();

    //        var bodyString = JsonSerializer.Serialize(createExcelMessage);

    //        var bodyByte = Encoding.UTF8.GetBytes(bodyString);

    //        var properties = channel.CreateBasicProperties();
    //        properties.Persistent = true;

    //        channel.BasicPublish(exchange: RabbitMQClientService_WorkerService.ExchangeName, routingKey: RabbitMQClientService_WorkerService.RoutingExcel, basicProperties: properties, body: bodyByte);
    //    }

    //    //public void Publish(EmailDto emailDtoMessage) // Düzelt
    //    //{
    //    //    var channel = _rabbitMQClientService.Connect();

    //    //    var bodyString = JsonSerializer.Serialize(emailDtoMessage);

    //    //    var bodyByte = Encoding.UTF8.GetBytes(bodyString);

    //    //    var properties = channel.CreateBasicProperties();
    //    //    properties.Persistent = true;

    //    //    channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingExcel, basicProperties: properties, body: bodyByte);
    //    //}
    //}
}
