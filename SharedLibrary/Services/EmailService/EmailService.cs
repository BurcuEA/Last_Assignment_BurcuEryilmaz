using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SharedLibrary.Dtos;

namespace SharedLibrary.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;


        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(EmailDto mailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:Username").Value));
            email.To.Add(MailboxAddress.Parse(mailRequest.To));
            email.Subject = mailRequest.Subject;

            var body = new TextPart(TextFormat.Plain) { Text = mailRequest.Body };

            //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", userFile.FilePath);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", "report-excel-fd3afdf-ee.xlsx"); // userFilePath GEREKLİ !!!

            var attachment = new MimePart() 
            {
                Content = new MimeContent(File.OpenRead(path)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };

          
            var multipart = new Multipart("mixed");

            multipart.Add(body);
            multipart.Add(attachment);

            email.Body = multipart;

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("Email:Host").Value, int.Parse(_config.GetSection("Email:Port").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("Email:Username").Value, _config.GetSection("Email:Password").Value);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
