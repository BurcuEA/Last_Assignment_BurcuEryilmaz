using Last_Assignment.Core.Configuration;
using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Last_Assignment.Core.Services;
using Last_Assignment.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;

namespace Last_Assignment.Service.Services
{
    public class AuthenticationService : IAuthenticationService // kendi namespace imizi seçmeliyiz dikkat et ... ÜYELİK sistemi içeren KARAR (C) // ctor kısımları 
    {
        private readonly List<Client> _clients; // (D) clientLoginDto
        private readonly ITokenService _tokenService;

        private readonly UserManager<UserApp> _userManager; //  ROLE // üyelik sistemi ilgili işlemler,kullanıcı var mı yok mu ..  (C) KAARAR ... User Manager ROLE - AUThentication service 1- 3:29 dk sn ...

        private readonly IUnitOfWork _unitOfWork;

        //IServie i çağırmıyoruz ,kendisi service zaten... AUThentication service 1- 4:17 dk sn ...
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;


        // appsettings ten okuyacağız o yüzden  IOptions<List<Client>> optionsClient kullandı ...
        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<UserApp> userManager,
            IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)  // userRefreshTokenRepositoryyerine userRefreshTokenService kullanımıştı ...
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;

        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);// Fail ile TokenDto null gidecek zaten ...// 400 client hatası, bizden kaynaklı olsaydı 500 dönerdik ... //  AUThentication service 1- 2:12 dk sn ...

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) // password hashleniyor ...
            {
                return Response<TokenDto>.Fail("Email or Password is wrong", 400, true); 
            }

            // Artık bir kullanıcı var o halde token oluşturabiliriz ...
            var token = _tokenService.CreateToken(user);

            // RefreshToken var mı kontrolü... daha önce vermiş miyiz diye ...
            // SingleOrDefaultAsync varsa gelecek yoksa da null ...
            var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token, 200);

        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto) // (D) clientLoginDto
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                //Async kullanmıyoruz ...
                return Response<ClientTokenDto>.Fail("Client Id or Client Secret not found", 404, false);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("Refresh token not found", 404, true);
            }

            // Refreshtoken varsa userId de vardır ...
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return Response<TokenDto>.Fail("User Id not found", 404, true);
            }

            var tokenDto = _tokenService.CreateToken(user);

            // bu token içinde hem access token hem de refresh token var ...
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(tokenDto, 200);
        }

        // Refresh token nuul yapcaz ki,kullanıcı logout olduğunda  //  AUThentication service 1 de sonlarda ..
        public async Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh token not found", 404, true);
            }

            _userRefreshTokenRepository.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);

            // 200 ü alırsa başarılı,almazsa bir sıkıntı meydana gelmiş demektir ...
        }
    }
}
