using Last_Assignment.Core.Models;

namespace Last_Assignment.Core.Repositories
{
    public interface ICustomerActivityRepository : IGenericRepository<CustomerActivity>
    {
        Task<List<CustomerActivity>> GetCustomerActivityWithCustomerAsync();     
    }
}
