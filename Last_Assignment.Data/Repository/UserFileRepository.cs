using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Microsoft.AspNetCore.Identity;
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
            //await _context.SaveChangesAsync();

            //   _context.Entry(((AppDbContext)_context).UserFiles).State = EntityState.Detached;

            return userfile;
        }

        public async Task<List<UserFile>> GetFilesAsync(string userId)
        {
            var userFile = await ((AppDbContext)_context).UserFiles.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToListAsync();

            return userFile;
        }

        //public async Task<List<ExcelDto>> GetExcelDtoAsync()
        //{
        //    var excelDtoList = (from cust in ((AppDbContext)_context).Customers
        //                        join custAct in ((AppDbContext)_context).CustomerActivities on cust.Id equals custAct.CustomerId
        //                        group custAct by new { cust.Name, cust.Surname, cust.PhoneNumber } into grp
        //                        select new ExcelDto()
        //                        {
        //                            Name = grp.Key.Name,
        //                            Surname = grp.Key.Surname,
        //                            PhoneNumber = grp.Key.PhoneNumber,
        //                            Count = grp.Where(c => c.CustomerId > 0).Count(),
        //                            TotalAmount = grp.Sum(c => c.Amount)
        //                        }).ToListAsync(); //Take(10);

        //    return await excelDtoList;
        //}
    }

}
