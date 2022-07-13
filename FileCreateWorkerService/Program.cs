using FileCreateWorkerService;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    //.ConfigureServices(services=> BERY
    .ConfigureServices((hostContext,services) =>
    {

        IConfiguration Configuration = hostContext.Configuration;



        services.AddDbContext<LastAssignmentDBContext>(x =>
        {
            x.UseNpgsql(Configuration.GetConnectionString("PostgresSqlServer"), npgsqloption =>
            {
                npgsqloption.MigrationsAssembly(Assembly.GetAssembly(typeof(LastAssignmentDBContext)).GetName().Name);
            });

        });
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
       


        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
