using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    //[Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        private readonly IGenericService<Customer, CustomerDto> _genericService;
        private readonly ICustomerService _customerService;

        public CustomerController(IGenericService<Customer, CustomerDto> genericService, ICustomerService customerService)
        {
            _genericService = genericService;
            _customerService = customerService;
        }

        [Authorize("Admin,Editor")]
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return ActionResultInstance(await _genericService.GetAllAsync());
        }

        [Authorize("Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> SaveCustomer(CustomerDto customerDto)
        {
            return ActionResultInstance(await _genericService.AddAsync(customerDto));
        }

        [Authorize("Admin,Editor")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customerDto)
        {
            return ActionResultInstance(await _genericService.UpdateAsync(customerDto, customerDto.Id));
        }

        [Authorize("Admin")]
        //api/customer/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            return ActionResultInstance(await _genericService.Remove(id));
        }

        [Authorize("Admin,Editor")]
        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> GetSingleCustomerByIdWithCustomerActivities(int customerId)  
        {
            return ActionResultInstance(await _customerService.GetSingleCustomerByIdWithCustomerActivitiesAsync(customerId));
        }
    }
}
