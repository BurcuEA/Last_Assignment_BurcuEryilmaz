using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using SharedLibrary.Dtos;

namespace Last_Assignment.Core.Services
{
    public interface ICustomerService : IGenericService<Customer, CustomerWithCustomerActivitiesDto>
    {
        Task<Response<CustomerWithCustomerActivitiesDto>> GetSingleCustomerByIdWithCustomerActivitiesAsync(int customerId);
    }
}
