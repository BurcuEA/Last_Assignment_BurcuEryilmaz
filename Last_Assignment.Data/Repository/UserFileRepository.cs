using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Last_Assignment.Data.Repository
{
    public class UserFileRepository : GenericRepository<UserFile>, IUserFileRepository
    {
        private readonly AppDbContext _context;
        public UserFileRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserFile> CreateUserFileAsync(string userId)
        {
            var fileName = $"report-excel-{Guid.NewGuid().ToString().Substring(1, 10)}";

            UserFile userfile = new()
            {
                UserId = userId,
                FileName = fileName,
                FileStatus = FileStatus.Creating,
                FilePath = "aaaa"
            };

            await _context.UserFiles.AddAsync(userfile);           

            return userfile;
        }

        public async Task<List<UserFile>> GetFilesAsync(string userId)
        {
            var userFile = await ((AppDbContext)_context).UserFiles.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToListAsync();

            return userFile;
        }
    }
}
