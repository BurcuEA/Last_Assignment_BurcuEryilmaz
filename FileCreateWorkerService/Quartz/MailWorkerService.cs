using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Dtos;
using SharedLibrary.RabbitMQModels;
using SharedLibrary.Services;
using System.Text;
using System.Text.Json;

namespace FileCreateWorkerService.Quartz
{
    internal class MailWorkerService : BackgroundService 
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public MailWorkerService(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
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
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    Console.WriteLine($"My Service is running at EMAIL {DateTime.Now}");
            //    await Task.Delay(2000, stoppingToken);
            //}

            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicConsume("direct-queue-Email", false, consumer);

            consumer.Received += Consumer_Received;

            return Task.CompletedTask;

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000);

            var emailRequest = JsonSerializer.Deserialize<EmailMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            var baseUrl = "https://localhost:7228/api/Email";

            using (var httpClient = new HttpClient())
            {
                try
                {
                   var response = await httpClient.PostAsync($"{baseUrl}?emailRequest={emailRequest}", null);
                 
                    if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"MAiLim ( Id : {emailRequest.Subject}) was sended by successful");
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
