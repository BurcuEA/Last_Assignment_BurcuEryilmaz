using SharedLibrary.Dtos;
using SharedLibrary.RabbitMQModels;

namespace SharedLibrary.Services.EmailService
{
    public interface IEmailService
    {
        Task<Response<NoDataDto>> SendEmailAsync(EmailMessage request);
    }
}
