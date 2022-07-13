using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Last_Assignment.Core.Services;
using Last_Assignment.Core.UnitOfWork;
using SharedLibrary.Dtos;

namespace Last_Assignment.Service.Services
{
    public class CustomerActivityService : GenericService<CustomerActivity, CustomerActivityWithCustomerDto>, ICustomerActivityService
    {
        private readonly ICustomerActivityRepository _customerActivityRepository;
        
        public CustomerActivityService(IGenericRepository<CustomerActivity> genericRepository, IUnitOfWork unitOfWork,  ICustomerActivityRepository customerActivityRepository) : base(unitOfWork, genericRepository)
        {
            _customerActivityRepository = customerActivityRepository;
        }

        public async Task<Response<List<CustomerActivityWithCustomerDto>>> GetCustomerActivityWithCustomerAsync()
        {
            var customerActivities = await _customerActivityRepository.GetCustomerActivityWithCustomerAsync();

            return Response<List<CustomerActivityWithCustomerDto>>.Success(ObjectMapper.Mapper.Map<List<CustomerActivityWithCustomerDto>>(customerActivities), 200);
        }
    }
}
