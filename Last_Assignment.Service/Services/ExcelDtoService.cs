using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Last_Assignment.Core.Services;
using Last_Assignment.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Dtos;

namespace Last_Assignment.Service.Services
{
    public class ExcelDtoService : GenericService<UserFile,UserFileDto>, IExcelDtoService
    {
        private readonly IExcelDtoRepository _excelDtoRepository;
        private readonly UserManager<UserApp> _userManager;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        IUnitOfWork _unitOfWork;

        public ExcelDtoService(IGenericRepository<UserFile> genericRepository, IExcelDtoRepository excelDtoRepository, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, RabbitMQPublisher rabbitMQPublisher) : base(unitOfWork, genericRepository)
        {
            _excelDtoRepository = excelDtoRepository;
            _userManager = userManager;
            _rabbitMQPublisher = rabbitMQPublisher;
             _unitOfWork=  unitOfWork;
        }

        public async Task<Response<UserFileDto>> GetFilesAsync(string userId)
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var user = await _userManager.FindByNameAsync(userId);
            var userFile = await CreateUserFileAsync(userId);

            //_rabbitMQPublisher.Publish(new CreateExcelMessage() { FileId = userFile.Id }); Tekrar açılacak
            // ---- TempData["StartCreatingExcel"] = true;

            return Response<UserFileDto>.Success(ObjectMapper.Mapper.Map<UserFileDto>(userFile), 200);

        }

        public async Task<UserFile> CreateUserFileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);   
            var userFile = await _excelDtoRepository.CreateUserFileAsync(user.Id);

           await _unitOfWork.CommitAsync();

            return userFile;
        }

        //public async Task<List<ExcelDto>> GetExcelDtoAsync()
        //{
        //    //return Response<List<ExcelDto>>.Success(200);
        //    return await _excelDtoRepository.GetExcelDtoAsync();
        //}

    }
}
