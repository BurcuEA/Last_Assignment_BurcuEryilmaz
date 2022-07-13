using AutoMapper;
using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;

namespace Last_Assignment.Service
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerActivityDto, CustomerActivity>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
            CreateMap<CustomerWithCustomerActivitiesDto, Customer>().ReverseMap();
            CreateMap<CustomerActivityWithCustomerDto, CustomerActivity>().ReverseMap();
            CreateMap<UserFileDto, UserFile>().ReverseMap();           
        }
    }
}
