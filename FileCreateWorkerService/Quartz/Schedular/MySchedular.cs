using FileCreateWorkerService.Quartz.Configurations;
using Quartz;
using Quartz.Spi;
//using RabbitMQ.Client;

namespace FileCreateWorkerService.Quartz.Schedular
{
    class MySchedular : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory _jobFactory;
        private readonly List<JobMetadata> _jobMetadatas;
        private readonly ISchedulerFactory _schedulerFactory;

        public MySchedular(ISchedulerFactory schedulerFactory, List<JobMetadata> jobMetadatas, IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
            _schedulerFactory = schedulerFactory;
            _jobMetadatas = jobMetadatas;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //Creating Schdeular
            Scheduler = await _schedulerFactory.GetScheduler();
            Scheduler.JobFactory = _jobFactory;

            //Support for Multiple Jobs
            _jobMetadatas?.ForEach(jobMetadata =>
            {
                //Create Job
                IJobDetail jobDetail = CreateJob(jobMetadata);
                //Create trigger
                ITrigger trigger = CreateTrigger(jobMetadata);
                //Schedule Job
                Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken).GetAwaiter();
                //Start The Schedular
            });
           
            await Scheduler.Start(cancellationToken);
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithCronSchedule(jobMetadata.CronExpression)
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType)
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown();
        }
    }
}
