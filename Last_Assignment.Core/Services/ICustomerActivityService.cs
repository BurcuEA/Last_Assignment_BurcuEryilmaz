using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.Services
{
    public interface ICustomerActivityService : IGenericService<CustomerActivity, CustomerActivityWithCustomerDto>
    {
        Task<Response<List<CustomerActivityWithCustomerDto>>> GetCustomerActivityWithCustomerAsync();     
    }
}
