using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Dtos;
using SharedLibrary.Services;
using System.Text;
using System.Text.Json;

namespace FileCreateWorkerService.Quartz
{
    internal class MyService : BackgroundService  // EmailWorker olarak değişecek
    {

        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public MyService(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // "direct-queue-Email"


            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    Console.WriteLine($"My Service is running at EMAIL {DateTime.Now}");
            //    await Task.Delay(2000, stoppingToken);
            //}


            var consumer = new AsyncEventingBasicConsumer(_channel);

            //_channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            _channel.BasicConsume("direct-queue-Email", false, consumer);

            consumer.Received += Consumer_Received;

            return Task.CompletedTask;

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000);

            var mailDtoMessage = JsonSerializer.Deserialize<EmailDto>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            #region MyRegion
//using var ms = new System.IO.MemoryStream();

            //var wb = new XLWorkbook();
            //var ds = new DataSet();
            //ds.Tables.Add(GetTable("ExcelReport"));

            //wb.Worksheets.Add(ds);
            //wb.SaveAs(ms);

            //MultipartFormDataContent multipartFormDataContent = new();

            //multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

            #endregion
            
            var baseUrl = "https://localhost:7228/api/Email";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    //var response = await httpClient.PostAsync($"{baseUrl}?fileId={createExcelMessage.FileId}", multipartFormDataContent);
                    var response = await httpClient.PostAsync($"{baseUrl}?emailRequest={mailDtoMessage}",null);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"MAiLim ( Id : {mailDtoMessage.Subject}) was created by successful");
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
                }
                catch (Exception ex)
                {
                    var hata = ex.Message;
                }
            }
        }

    }
}
