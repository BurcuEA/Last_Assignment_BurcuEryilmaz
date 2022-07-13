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
    }
}
