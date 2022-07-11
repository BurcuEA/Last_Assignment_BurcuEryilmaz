﻿using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Last_Assignment.Data.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Customer> GetSingleCustomerByIdWithCustomerActivitiesAsync(int customerId)
        {
           return await ((Last_Assignment.Data.AppDbContext)_context).Customers.Include(x => x.CustomerActivities).Where(x => x.Id == customerId).SingleOrDefaultAsync();
            // return await _dbSet.SingleOrDefaultAsync();

           // return await GetByIdAsync(customerId);
            

        }
    }
}
