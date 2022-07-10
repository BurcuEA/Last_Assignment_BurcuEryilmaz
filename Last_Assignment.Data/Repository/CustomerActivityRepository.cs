using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Last_Assignment.Data.Repository
{
    public class CustomerActivityRepository : GenericRepository<CustomerActivity>, ICustomerActivityRepository
    {
        public CustomerActivityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<CustomerActivity>> GetCustomerActivityWithCustomerAsync()
        {
            //return await _context.Products.Include(x => x.Category).ToListAsync();
            return await _dbSet.ToListAsync();
        }
    }
}
