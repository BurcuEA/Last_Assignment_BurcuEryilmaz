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
    public class CustomerActivityController : CustomBaseController
    {
        private readonly IGenericService<CustomerActivity, CustomerActivityDto> _genericService; 
        private readonly ICustomerActivityService _customerActivityService;
        public CustomerActivityController(IGenericService<CustomerActivity, CustomerActivityDto> genericService, ICustomerActivityService customerActivityService)
        {
            _genericService = genericService;
            _customerActivityService = customerActivityService;
        }

        [Authorize("Admin,Editor")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerActivities()
        {
            return ActionResultInstance(await _genericService.GetAllAsync());
        }

        [Authorize("Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> SaveCustomerActivity(CustomerActivityDto customerActivityDto)
        {
            return ActionResultInstance(await _genericService.AddAsync(customerActivityDto));
        }

        [Authorize("Admin,Editor")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomerActivity(CustomerActivityDto customerActivityDto)
        {
            return ActionResultInstance(await _genericService.UpdateAsync(customerActivityDto, customerActivityDto.Id));
        }

        [Authorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerActivity(int id)
        {
            return ActionResultInstance(await _genericService.Remove(id));
        }

        [Authorize("Admin,Editor")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerActivityWithCustomer()
        {
            return ActionResultInstance(await _customerActivityService.GetCustomerActivityWithCustomerAsync());
        }

    }
}
