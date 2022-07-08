using Last_Assignment.Core.DTOs;
using SharedLibrary.Dtos;

namespace Last_Assignment.Core.Services
{
    // kimlik doğrulama işlemini gerçekleştirecek
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken);
        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
        /*     // Token üreten API için  ---> ClientId Client Secret lar dizi halinde tutuluyor ,eğer çok fazla( 5 ten fazlaysa,10-20 vs) client olsaydı VT de tutmak daha uygun olurdu.Şimdilik appsettings içinde tutuyor olacağız.Client token döneceğiz,refreshtoken a gerek yok.
          Client token döneceğiz,refreshtoken a gerek yok çünkü Üyelik sistemi olmadığında refreshtoken a gerek yok .............   !!!!
          token ın ömrü dolduğu anda  Çünkü elimizde client Id ve ClientSecret var... */
    }
}
