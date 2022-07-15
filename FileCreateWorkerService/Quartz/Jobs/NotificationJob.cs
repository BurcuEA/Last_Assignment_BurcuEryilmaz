using Quartz;
using SharedLibrary.Services.EmailService;

namespace FileCreateWorkerService.Jobs
{
    //public class SendMailJob : IJob
    //{
    //    private readonly IEmailService _emailService;
    //    private readonly ILogger<SendMailJob> _logger;

    //    public SendMailJob(IEmailService emailService, ILogger<SendMailJob> logger)
    //    {
    //        _emailService = emailService;
    //        _logger = logger;
    //    }
    //    public  Task Execute(IJobExecutionContext context)
    //    {
    //        //await _emailService.SendEmailAsync(request);
    //        _logger.LogInformation($"SendEmail Job: Mail Send at {DateTime.Now} and Jobtype: {context.JobDetail.JobType}");
    //        return  Task.CompletedTask;
    //    }

    //}


    class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        public NotificationJob(ILogger<NotificationJob> logger)
        {
           _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notification Job: Notify User at {DateTime.Now} and Jobtype: {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }
    }
}
