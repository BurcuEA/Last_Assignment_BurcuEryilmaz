using RabbitMQ.Client;
using SharedLibrary.RabbitMQModels;
using System.Text;
using System.Text.Json;

namespace SharedLibrary.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public static string ExchangeName = "ExcelMailDirectExchange";
        
        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(object obj,string whichQueueRoute)
        {
            var message = obj;

            if (obj is CreateExcelMessage)
            {
                 message = (CreateExcelMessage)obj;
            }
            else
            {
                message = (EmailMessage)obj;
            }

            var channel = _rabbitMQClientService.Connect();
            
            channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
           
            var RoutingKey = $"route-{whichQueueRoute}";
            var QueueName = $"direct-queue-{whichQueueRoute}";

            channel.QueueDeclare(QueueName, true, false, false, null);
            channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);

            var bodyString = JsonSerializer.Serialize(message);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: properties, body: bodyByte);
        }
    }
}
