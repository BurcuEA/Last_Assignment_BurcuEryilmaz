using AutoMapper;
using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;

namespace Last_Assignment.Service
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            //Product ve User entity old mapledi aldı...

            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerActivityDto, CustomerActivity>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}
