using FileCreateWorkerService;
using FileCreateWorkerService.Jobs;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Quartz.Configurations;
using FileCreateWorkerService.Quartz.JobFactory;
using FileCreateWorkerService.Quartz.Schedular;
using FileCreateWorkerService.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using RabbitMQ.Client;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
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



        #region "Quartz"

        //var tokenOption = Configuration.GetSection("Email").Get<CustomTokenOption>(); Bunu burda kullanmaya çalýþ Email için


        services.AddSingleton<IJobFactory, MyJobFactory>();
        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

        #region Adding JobType
        services.AddSingleton<NotificationJob>(); // SendEmailJob
                                                  //services.AddSingleton<LoggerJob>();
        #endregion

        #region Adding Jobs 
        List<JobMetadata> jobMetadatas = new List<JobMetadata>();
        jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0/10 * * * * ?"));
        //jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(LoggerJob), "Log Job", "0/5 * * * * ?"));

        services.AddSingleton(jobMetadatas);
        #endregion


        services.AddHostedService<MySchedular>();


        #endregion




        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

        services.AddHostedService<Worker>();


        //services.AddSingleton<RabbitMQClientService_Quartz>();
        //services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

        //services.AddHostedService<MyService>();
    })
    .Build();

await host.RunAsync();
