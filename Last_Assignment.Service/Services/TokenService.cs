using Last_Assignment.Core.Configuration;
using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Services; //
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options; //
using Microsoft.IdentityModel.Tokens; //
using SharedLibrary.Configurations; //
using SharedLibrary.Services;
using System.IdentityModel.Tokens.Jwt; //
using System.Security.Claims;
using System.Security.Cryptography; //

namespace Last_Assignment.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        // 32 bytelık random string değer üretecek ...
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            //random bir değer üretiliyor
            using var rnd = RandomNumberGenerator.Create();

            //Random değeri byte larını al ve numberByte a aktar
            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);

            //ikinci yöntem
            //return Guid.NewGuid().ToString();
        } 


        //Üyelik sistemi gerektiren
        private IEnumerable<Claim> GetClaims(UserApp userApp, List<String> audiences)
        {
            //kullanıcı ile ilgili claim ler
            var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,userApp.Id), // ID ye karşılık geliyor,Kimlik
            new Claim(JwtRegisteredClaimNames.Email,userApp.Email), // "email"
            new Claim(ClaimTypes.Name,userApp.UserName),
           
            //new Claim(ClaimTypes.Role), ???  karar ver Identityserver 4 te varmış  ---Token Service 2 --7:56 dk:sn
            //new Claim(ClaimTypes.Role,"Role"), ???  karar ver Identityserver 4 te varmış

            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) //json ı kimliklendirecek bir identity,random--Token Id si olsun ...
            };

            // client lar için claim ler ...
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x))); // Select foreach yerine

            return userList;
        }

        //Üyelik sistemi gerektirmeyen ?? !!  karar verrr -silinebilir -- token service 3 ,1. dk falan  
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());  ///// 'XXXXXXXX BRC'  Subject özne

            return claims;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials); // BRC-ROLE kısmını burda mı versem KARAR ... Token Service 4 - 5.dk falan ...

            var handler = new JwtSecurityTokenHandler(); // token oluşturacak

            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;

            //Bunun API dan haberi olmayacak,AuthenticationService kullanacak ...  Token Service 4 



        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            // KARAR üyülik sitemi ile ilgili kalacak mı kalmayacak mı BRC
            
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration
            };

            return tokenDto;

            // üyelik sitemiyle ilgili bilgi barındırmıyor,tamamiyle kendisiyle ilgili bilgi barındırıyor ... token service 5 -- 1:27 s. yaklaşık olarak
        }
    }
}
