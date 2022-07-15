using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Last_Assignment.Core.Services;
using Last_Assignment.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using SharedLibrary;
using SharedLibrary.Dtos;
using SharedLibrary.Services;

namespace Last_Assignment.Service.Services
{
    public class UserFileService : GenericService<UserFile,UserFileDto>, IUserFileService
    {
        private readonly IUserFileRepository _userFileRepository;
        private readonly UserManager<UserApp> _userManager;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        IUnitOfWork _unitOfWork;

        public UserFileService(IGenericRepository<UserFile> genericRepository, IUserFileRepository userFileRepository, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, RabbitMQPublisher rabbitMQPublisher) : base(unitOfWork, genericRepository)
        {
            _userFileRepository = userFileRepository;
            _userManager = userManager;
            _rabbitMQPublisher = rabbitMQPublisher;
             _unitOfWork=  unitOfWork;
        }

        public async Task<Response<UserFileDto>> GetFilesAsync(string userId)
        {
            var userFile = await CreateUserFile(userId);

            _rabbitMQPublisher.Publish(new CreateExcelMessage() { FileId = userFile.Id },"Excel"); 
            //_rabbitMQPublisher.Publish(new EmailDto() {  });

            return Response<UserFileDto>.Success(ObjectMapper.Mapper.Map<UserFileDto>(userFile), 200);
        }

        public async Task<UserFile> CreateUserFile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);   
            var userFile = await _userFileRepository.CreateUserFileAsync(user.Id);

           await _unitOfWork.CommitAsync();

            return userFile;
        }      
    }
}
