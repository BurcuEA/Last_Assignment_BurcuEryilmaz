using AutoMapper;
using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Last_Assignment.Core.Services;
using Last_Assignment.Core.UnitOfWork;
using SharedLibrary.Dtos;

namespace Last_Assignment.Service.Services
{
    public class CustomerService : GenericService<Customer, CustomerWithCustomerActivitiesDto>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        //private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer> genericRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository) : base(unitOfWork,genericRepository)
        {
            //_mapper = mapper;
            _customerRepository = customerRepository;
        }

       public async Task<Response<CustomerWithCustomerActivitiesDto>> GetSingleCustomerByIdWithCustomerActivitiesAsync(int customerId)
        {
            var customer = await _customerRepository.GetSingleCustomerByIdWithCustomerActivitiesAsync(customerId);
            //var customerDto = _mapper.Map<CustomerWithCustomerActivitiesDto>(customer);

            return Response<CustomerWithCustomerActivitiesDto>.Success(ObjectMapper.Mapper.Map<CustomerWithCustomerActivitiesDto>(customer), 200);

        }
    }
}
