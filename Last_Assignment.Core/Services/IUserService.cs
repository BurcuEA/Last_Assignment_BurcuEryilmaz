using Last_Assignment.Core.DTOs;
using SharedLibrary.Dtos;

namespace Last_Assignment.Core.Services
{
    // Kullanıcıların kaydedilmesi de gerekiyor.Üyeler önce VT ye kaydedilmeli  --SaveUser endpointi olmalı kullanıcılar kaydedilmeli varsa giriş yapabilsin
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
    }
}
