using Last_Assignment.Core.Models;
using Last_Assignment.Data;
using Last_Assignment.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        //private readonly RabbitMQPublisher_Quartz _rabbitMQPublisherQuartz;
        public FilesController(AppDbContext context, RabbitMQPublisher rabbitMQPublisher)
        {
            _context = context;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int fileId)
        {
            if (file is not { Length: > 0 }) return BadRequest();

            var userFile = await _context.UserFiles.FirstAsync(x => x.Id == fileId);
            var filePath = userFile.FileName + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", filePath);

            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            userFile.CreatedDate = DateTime.Now;
            userFile.FilePath = filePath;
            userFile.FileStatus = FileStatus.Completed;

            await _context.SaveChangesAsync();



            #region "Email Publisher // BERY" 

            //var user = await _context.Users.FirstAsync(x => x.Id == userFile.UserId);

            //_rabbitMQPublisher.Publish(new EmailDto() { FileId = userfile.Id });
            //_rabbitMQPublisher.Publish(new EmailDto() { To = user.Email ,Subject="",Body="" });
            #endregion

          



            return Ok();
        }
    }
}
