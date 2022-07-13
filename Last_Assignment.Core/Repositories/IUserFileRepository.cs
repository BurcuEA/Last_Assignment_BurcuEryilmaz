using Last_Assignment.Core.Models;

namespace Last_Assignment.Core.Repositories
{
    public interface IUserFileRepository
    {
        Task<UserFile> CreateUserFileAsync(string userId);
        Task<List<UserFile>> GetFilesAsync(string userId);
    }
}
