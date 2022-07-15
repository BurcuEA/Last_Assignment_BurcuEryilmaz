using Quartz;
using SharedLibrary.Services.EmailService;

namespace FileCreateWorkerService.Jobs
{
    public class SendMailJob : IJob
    {
        private readonly ILogger<SendMailJob> _logger;

        public SendMailJob(ILogger<SendMailJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {            
            //await _emailService.SendEmailAsync(request);
            _logger.LogInformation($"SendEmail Job: Mail Send at {DateTime.Now} and Jobtype: {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }

    }
}
