namespace FileCreateWorkerService.Quartz
{
    internal class MyService : BackgroundService  // EmailWorker olarak değişecek
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    Console.WriteLine($"My Service is running at EMAIL {DateTime.Now}");
            //    await Task.Delay(2000, stoppingToken);
            //}
        }
    }



    //public class MyService : BackgroundService
    //{
    //    private readonly ILogger<Worker> _logger;
    //    private readonly RabbitMQClientService _rabbitMQClientService;
    //    private readonly IServiceProvider _serviceProvider;
    //    private IModel _channel;


    //    public MyService(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
    //    {
    //        _logger = logger;
    //        _rabbitMQClientService = rabbitMQClientService;
    //        _serviceProvider = serviceProvider;
    //    }

    //    //public override Task StartAsync(CancellationToken cancellationToken)
    //    //{
    //    //    //_channel = _rabbitMQClientService.Connect();
    //    //    //_channel.BasicQos(0, 1, false);

    //    //    //return base.StartAsync(cancellationToken);
    //    //}

    //    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        var consumer = new AsyncEventingBasicConsumer(_channel);

    //        _channel.BasicConsume("EmailQueue"+ RabbitMQClientService.QueueName, false, consumer);  /// düzelt EmailQueue

    //        consumer.Received += Consumer_Received;

    //        return Task.CompletedTask;
    //    }

    //    private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
    //    {
    //        await Task.Delay(5000);

    //        //var createExcelMessage = JsonSerializer.Deserialize<CreateExcelMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));
    //        var emailDtoMessage = JsonSerializer.Deserialize<EmailDto>(Encoding.UTF8.GetString(@event.Body.ToArray()));

    //        //using var ms = new System.IO.MemoryStream();

    //        //var wb = new XLWorkbook();
    //        //var ds = new DataSet();
    //        //ds.Tables.Add(GetTable("ExcelReport"));

    //        //wb.Worksheets.Add(ds);
    //        //wb.SaveAs(ms);

    //        //MultipartFormDataContent multipartFormDataContent = new();

    //        //multipartFormDataContent.Add((new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

    //        //var baseUrl = "https://localhost:7228/api/Files";
    //        var baseUrl = "https://localhost:7228/api/Email";

    //        using (var httpClient = new HttpClient())
    //        {
    //            EmailDto emailDto = new()
    //            {
    //                To = emailDtoMessage.To,
    //                Subject = emailDtoMessage.Subject,
    //                Body = emailDtoMessage.Body
    //            };

    //            try
    //            {
    //                //  var response = await httpClient.PostAsync($"{baseUrl}?fileId={createExcelMessage.FileId}", multipartFormDataContent);
    //                var response = await httpClient.PostAsync($"{baseUrl}?request={emailDto}", null);

    //                if (response.IsSuccessStatusCode)
    //            {
    //                _logger.LogInformation($"Emaillll gönderrr ( Id : {emailDto.Subject}) konulu mesaj gönderildi was created by successful"); /// düzelt 
    //                _channel.BasicAck(@event.DeliveryTag, false);
    //            }
    //            }
    //            catch (Exception ex)
    //            {
    //                var hata = ex.Message;
    //            }
    //        }
    //    }

    //    //private DataTable GetTable(string tableName)
    //    //{
    //    //    List<ReportListDto> reportList;

    //    //    using (var scope = _serviceProvider.CreateScope())
    //    //    {
    //    //        var context = scope.ServiceProvider.GetRequiredService<LastAssignmentDBContext>();

    //    //        reportList = (from cust in context.Customers
    //    //                      join custAct in context.CustomerActivities on cust.Id equals custAct.CustomerId
    //    //                      group custAct by new { cust.Id, cust.Name, cust.Surname, cust.PhoneNumber } into grp
    //    //                      select new ReportListDto()
    //    //                      {
    //    //                          CustomerId = grp.Key.Id,
    //    //                          Name = grp.Key.Name,
    //    //                          Surname = grp.Key.Surname,
    //    //                          PhoneNumber = grp.Key.PhoneNumber,
    //    //                          Count = grp.Where(c => c.CustomerId > 0).Count(),
    //    //                          TotalAmount = grp.Sum(c => c.Amount)
    //    //                      }).ToList();
    //    //    }

    //    //    DataTable table = new DataTable { TableName = tableName };

    //    //    table.Columns.Add("CustomerId", typeof(int));
    //    //    table.Columns.Add("Name", typeof(String));
    //    //    table.Columns.Add("Surname", typeof(string));
    //    //    table.Columns.Add("PhoneNumber", typeof(string));
    //    //    table.Columns.Add("Count", typeof(int));
    //    //    table.Columns.Add("TotalAmount", typeof(decimal));

    //    //    reportList.ForEach(x =>
    //    //    {
    //    //        table.Rows.Add(x.CustomerId, x.Name, x.Surname, x.PhoneNumber, x.Count, x.TotalAmount);
    //    //    });

    //    //    return table;
    //    //}
    //}
}
