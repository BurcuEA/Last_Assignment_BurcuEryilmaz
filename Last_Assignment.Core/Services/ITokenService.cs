using Last_Assignment.Core.Configuration;
using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;

namespace Last_Assignment.Core.Services
{
    //Dış dünyaya açılmayacak
    //Response dönmüyoruz,ITokenService'i kendi içinde kullanıyoruz,IGenericService gibi değil yani
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);   
        ClientTokenDto CreateTokenByClient(Client client); 
    }
}
