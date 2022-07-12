using FileCreateWorkerService;
using FileCreateWorkerService.Services;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    //.ConfigureServices(services=> BERY
    .ConfigureServices((hostContext,services) =>
    {

        IConfiguration Configuration = hostContext.Configuration;



        //services.AddDbContext<AppDbContext>(x =>
        //{
           

        //    x.UseNpgsql(Configuration.GetConnectionString("PostgresSqlServer"), npgsqloption =>
        //    {

        //        npgsqloption.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

        //    });

        //});
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);




        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
       


        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
