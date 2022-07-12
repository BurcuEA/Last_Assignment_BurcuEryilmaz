using Last_Assignment.Core.Models;

namespace Last_Assignment.Core.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetSingleCustomerByIdWithCustomerActivitiesAsync(int customerId);        
    }
}
