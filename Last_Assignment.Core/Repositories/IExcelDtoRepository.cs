using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;

namespace Last_Assignment.Core.Repositories
{
    public interface IExcelDtoRepository
    {
        //Task<List<ExcelDto>> GetExcelDtoAsync();
        Task<UserFile> CreateUserFileAsync(string userId);
        Task<List<UserFile>> GetFilesAsync(string userId);
    }
}
