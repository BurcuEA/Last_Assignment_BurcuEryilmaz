using Last_Assignment.Core.DTOs;
using SharedLibrary.Dtos;

namespace Last_Assignment.Core.Services
{
    // Kullanıcıların kaydedilmesi de gerekiyor.Üyeler önce VT ye kaydedilmeli  --SaveUser endpointi olmalı kullanıcılar kaydedilmeli varsa giriş yapabilsin
    // Repository oluşturulmadı dikkat et -- (16 vd -1:35) USerManager - Identity kütüphanesi sayasinde -- User Manager ,Role Manager , Sign..ing Manager
    /* Response dönülecek..
    AuthServer.API nin kullanıcı oluşturulan bir controler ının constructor ınd bu servis geçiyor olacak*/
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
    }
}
