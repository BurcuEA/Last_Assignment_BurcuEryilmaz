
using RabbitMQ.Client;
using SharedLibrary.Dtos;
using System.Text;
using System.Text.Json;

namespace SharedLibrary.Services
{
    //public enum PubNames
    //{
    //    ExcelRouteQueue = 1,
    //    MailRouteQueue = 2
    //}

    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public static string ExchangeName = "ExcelDirectExchange";
        //public static string RoutingExcel = "excel-route-file";
        //public static string RoutingKey = "excel-route-file";
        //public static string QueueName = "queue-excel-file";

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
                message = (EmailDto)obj;
            }


            var channel = _rabbitMQClientService.Connect();

            //BERY
            #region exchange-queue-routingKey 

            channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            // channel.ExchangeDeclare(ExchangeName,durable:true, type: "direct", true, false);


            var RoutingKey = $"route-{whichQueueRoute}";
            var QueueName = $"direct-queue-{whichQueueRoute}";

            channel.QueueDeclare(QueueName, true, false, false, null);
            channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);


            var bodyString = JsonSerializer.Serialize(message);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            //channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingExcel, basicProperties: properties, body: bodyByte);


            channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: properties, body: bodyByte);


            #region MyRegion
            //Enum.GetNames(typeof(PubNames)).ToList().ForEach(x =>
            //{
            //    var RoutingKey = $"route-{x}";
            //    var QueueName = $"direct-queue-{x}";
            //    channel.QueueDeclare(QueueName, true, false, false, null);
            //    channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);

            //});

            //PubNames pubName = (PubNames)new Random().Next(1, 2); // 2--3

            //var RoutingKey = $"route-{x}";

            #endregion

            #endregion


            #region Message Body  xxxxx

            //var bodyString = JsonSerializer.Serialize(createExcelMessage);
            //var bodyString = JsonSerializer.Serialize(message);

            //var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            //var properties = channel.CreateBasicProperties();
            //properties.Persistent = true;

            //channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingExcel, basicProperties: properties, body: bodyByte);

            #endregion


        }

        //public void Publish(EmailDto emailDtoMessage) // Düzelt
        //{
        //    var channel = _rabbitMQClientService.Connect();

        //    var bodyString = JsonSerializer.Serialize(emailDtoMessage);

        //    var bodyByte = Encoding.UTF8.GetBytes(bodyString);

        //    var properties = channel.CreateBasicProperties();
        //    properties.Persistent = true;

        //    channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingExcel, basicProperties: properties, body: bodyByte);
        //}
    }
}
