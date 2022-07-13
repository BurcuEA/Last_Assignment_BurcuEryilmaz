using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using SharedLibrary.Dtos;

namespace Last_Assignment.Core.Services
{
    public interface ICustomerActivityService : IGenericService<CustomerActivity, CustomerActivityWithCustomerDto>
    {
        Task<Response<List<CustomerActivityWithCustomerDto>>> GetCustomerActivityWithCustomerAsync();     
    }
}
