using Last_Assignment.Core.Configuration;
using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;

namespace Last_Assignment.Core.Services
{
    //Dış dünyaya açılmayacak
    //Response dönmüyoruz,ITokenService'i kendi içinde kullanıyoruz,IGenericService gibi değil yani
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);   // Herhangibir kullanıcı için oluşturulacak.Bu user a özgü bir token oluşturulacak

        ClientTokenDto CreateTokenByClient(Client client); // Burdaki client telefone ya da bilgisar bibi düşün
    }
}
