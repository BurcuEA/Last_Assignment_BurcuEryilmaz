using Quartz;

namespace FileCreateWorkerService.Jobs
{
    //public class SendMailJob : IJob
    //{
    //    private readonly IEmailService _emailService;

    //    public SendMailJob(IEmailService emailService)
    //    {
    //        _emailService = emailService;
    //    }
    //    public async Task Execute(IJobExecutionContext context)
    //    {

    //        //await _emailService.SendEmailAsync(request);

    //    }

    //}


    class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        public NotificationJob(ILogger<NotificationJob> logger)
        {
            this._logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notification Job: Notify User at {DateTime.Now} and Jobtype: {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }
    }
}
