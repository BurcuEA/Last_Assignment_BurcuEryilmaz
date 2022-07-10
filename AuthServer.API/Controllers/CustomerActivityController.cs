using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerActivityController : CustomBaseController
    {
        private readonly IGenericService<CustomerActivity, CustomerActivityDto> _customerActivityService;

        public CustomerActivityController(IGenericService<CustomerActivity, CustomerActivityDto> customerActivityService)
        {
            _customerActivityService = customerActivityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return ActionResultInstance(await _customerActivityService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(CustomerActivityDto customerActivityDto)
        {
            return ActionResultInstance(await _customerActivityService.AddAsync(customerActivityDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(CustomerActivityDto customerActivityDto)
        {
            return ActionResultInstance(await _customerActivityService.UpdateAsync(customerActivityDto, customerActivityDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return ActionResultInstance(await _customerActivityService.Remove(id));
        }

    }
}
