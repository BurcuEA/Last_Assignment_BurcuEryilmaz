using Microsoft.AspNetCore.Mvc;
using SharedLibrary.RabbitMQModels;
using SharedLibrary.Services.EmailService;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailMessage emailRequest) // userFilePath GEREKLİİİ!!!
        {
            await _emailService.SendEmailAsync(emailRequest);

            return Ok();
        }
    }
}
