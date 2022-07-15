using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Dtos;
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
        public async Task<IActionResult> SendEmail(EmailDto emailRequest) // userFilePath GEREKLİİİ!!!
        {
            await _emailService.SendEmailAsync(emailRequest);


          //    //_rabbitMQPublisher.Publish(new CreateExcelMessage() { FileId = userFile.Id });
          //_rabbitMQPublisher.Publish(new EmailDto() {  });


            return Ok();
        }
    }
}
