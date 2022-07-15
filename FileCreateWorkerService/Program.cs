using FileCreateWorkerService;
using FileCreateWorkerService.Jobs;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Quartz;
using FileCreateWorkerService.Quartz.Configurations;
using FileCreateWorkerService.Quartz.JobFactory;
using FileCreateWorkerService.Quartz.Schedular;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using RabbitMQ.Client;
using SharedLibrary.Services;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration Configuration = hostContext.Configuration;

        services.AddDbContext<LastAssignmentDBContext>(x =>
        {
            x.UseNpgsql(Configuration.GetConnectionString("PostgresSqlServer"), npgsqloption =>
            {
                npgsqloption.MigrationsAssembly(Assembly.GetAssembly(typeof(LastAssignmentDBContext)).GetName().Name);
            });

        });

        #region "Quartz"

        services.AddSingleton<IJobFactory, MyJobFactory>();
        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

        #region Adding JobType
        services.AddSingleton<SendMailJob>();
        #endregion

        #region Adding Jobs 
        List<JobMetadata> jobMetadatas = new List<JobMetadata>();

        #region "0 7 1 1W * ?"
        
        /*
        Seconds 0
        Minutes 7
        Hours 1
        Day - of - month 1W weekday nearest the first of the month
        Month*
        Day - of - Week ? no specific value

        */
        #endregion

        // jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(SendMailJob), "Send Mail Job", "0 7 1 1W * ?"));
       jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(SendMailJob), "Send Mail Job", "0/10 * * * * ?")); // her 10 saniye

        #region run every 15 seconds

        //services.AddQuartz(q =>
        //{
        //    q.UseMicrosoftDependencyInjectionJobFactory();
        //    q.ScheduleJob<SendMailJob>(trigger => trigger
        //        .WithIdentity("SendRecurringMailTrigger")
        //        .WithSimpleSchedule(s =>
        //            s.WithIntervalInSeconds(15)
        //            .RepeatForever()
        //        )
        //        .WithDescription("This trigger will run every 15 seconds to send emails.")
        //    );
        //});

        #endregion

        services.AddSingleton(jobMetadatas);
        #endregion

        services.AddHostedService<MySchedular>();

        #endregion


        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

        services.AddHostedService<Worker>();

        services.AddHostedService<MailWorkerService>();
    })
    .Build();

await host.RunAsync();
