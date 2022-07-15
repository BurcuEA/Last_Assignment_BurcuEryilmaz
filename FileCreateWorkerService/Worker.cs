using ClosedXML.Excel;
using FileCreateWorkerService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Dtos.ExcelReportsDtos;
using SharedLibrary.RabbitMQModels;
using SharedLibrary.Services;
using System.Data;
using System.Text;
using System.Text.Json;

namespace FileCreateWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
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
            var consumer = new AsyncEventingBasicConsumer(_channel);

            //_channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            _channel.BasicConsume("direct-queue-Excel", false, consumer);

            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000);

             var createExcelMessage = JsonSerializer.Deserialize<CreateExcelMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            using var ms = new System.IO.MemoryStream();

            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable("ExcelReport"));

            wb.Worksheets.Add(ds);
            wb.SaveAs(ms);

            MultipartFormDataContent multipartFormDataContent = new();

            multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

            var baseUrl = "https://localhost:7228/api/Files";

            using (var httpClient = new HttpClient())
            {
                //try
                //{
                var response = await httpClient.PostAsync($"{baseUrl}?fileId={createExcelMessage.FileId}", multipartFormDataContent);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"File ( Id : {createExcelMessage.FileId}) was created by successful");
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
                //}
                //catch (Exception ex)
                //{
                //    var hata = ex.Message;
                //}
            }
        }

        private DataTable GetTable(string tableName)
        {
            List<GeneralReportListDto> reportList;

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LastAssignmentDBContext>();

                reportList = (from cust in context.Customers
                              join custAct in context.CustomerActivities on cust.Id equals custAct.CustomerId
                              group custAct by new { cust.Id, cust.Name, cust.Surname, cust.PhoneNumber } into grp
                              select new GeneralReportListDto()
                              {
                                  CustomerId = grp.Key.Id,
                                  Name = grp.Key.Name,
                                  Surname = grp.Key.Surname,
                                  PhoneNumber = grp.Key.PhoneNumber,
                                  Count = grp.Where(c => c.CustomerId > 0).Count(),
                                  TotalAmount = grp.Sum(c => c.Amount)
                              }).ToList();
            }

            DataTable table = new DataTable { TableName = tableName };

            table.Columns.Add("CustomerId", typeof(int));
            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Surname", typeof(string));
            table.Columns.Add("PhoneNumber", typeof(string));
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("TotalAmount", typeof(decimal));

            reportList.ForEach(x =>
                {
                    table.Rows.Add(x.CustomerId, x.Name, x.Surname, x.PhoneNumber, x.Count, x.TotalAmount);
                });

            return table;
        }

        private DataTable GetMontlyReportListTable(string tableName)
        {
            List<MontlyReportListDto> reportList;

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LastAssignmentDBContext>();

                // reportList = _context.Customers.Select(c => new MontlyReportListDto() { City = c.City}).GroupBy(c => c.City).ToList();
                reportList = (from cust in context.Customers
                              group cust by new { cust.City } into grp
                              select new MontlyReportListDto()
                              {
                                  City = grp.Key.City,
                                  CustomerCount = grp.Where(c => c.Id > 0).Count()
                              }).ToList();
            }

            DataTable table = new DataTable { TableName = tableName };

            table.Columns.Add("City", typeof(String));
            table.Columns.Add("CustomerCount", typeof(int));

            reportList.ForEach(x =>
            {
                table.Rows.Add(x.City, x.CustomerCount);
            });

            return table;
        }

        private DataTable GetWeeklyReportListTable(string tableName)
        {
            List<WeeklyReportListDto> reportList;

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LastAssignmentDBContext>();

                reportList = (from cust in context.Customers
                              join custAct in context.CustomerActivities on cust.Id equals custAct.CustomerId
                              group custAct by new { cust.Name, cust.Surname, cust.City } into grp
                              select new WeeklyReportListDto()
                              {
                                  Name = grp.Key.Name,
                                  Surname = grp.Key.Surname,
                                  TotalActivityCount = grp.Where(c => c.Id > 0).Count()
                              })
                              .OrderByDescending(c => c.TotalActivityCount)
                              .Take(5)
                              .ToList();
            }

            DataTable table = new DataTable { TableName = tableName };

            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Surname", typeof(string));
            table.Columns.Add("TotalActivityCount", typeof(int));

            reportList.ForEach(x =>
            {
                table.Rows.Add(x.Name, x.Surname, x.TotalActivityCount);
            });

            return table;
        }

        private DataTable GetSamePhoneDifferentNameReportListTable(string tableName)
        {
            List<GetSamePhoneDifferentNameReportListDto> reportList;

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LastAssignmentDBContext>();

                reportList = context.Customers
                      .GroupBy(c => c.PhoneNumber)
                      .Where(g => g.Count() > 1)
                      .Select(g => new GetSamePhoneDifferentNameReportListDto()
                      {
                          PhoneNumber = g.Key,
                          Count = g.Where(x => x.Id > 0).Count()
                      }).ToList();
            }

            DataTable table = new DataTable { TableName = tableName };

            table.Columns.Add("PhoneNumber", typeof(String));
            table.Columns.Add("Count", typeof(int));

            reportList.ForEach(x =>
            {
                table.Rows.Add(x.PhoneNumber, x.Count);
            });

            return table;
        }
    }
}