using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    // AuthenticationService ile haberleşecek ...
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //api/auth/createtoken
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            // sonra sil
            #region "ActionResultInstance sayesinde aşğ ne gerek kalmadı" 

            //if (result.StatusCode == 200)
            //{
            //    return Ok(result);
            //}
            //else if (result.StatusCode == 404)
            //{
            //    return NotFound();
            //}

            #endregion

            return ActionResultInstance(result);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = _authenticationService.CreateTokenByClient(clientLoginDto);

            // generic vermeye gerek yok burdan kendi T tipini çıkarıyor anlıyor ... result a bak !! ...
            return ActionResultInstance(result);
        }

        [HttpPost]
        //public async Task<IActionResult> RevokeRefreshToken(string refreshTokenDto) class daha uygun çünkü ilerde  refreshTokenDto dışında ihtiyaç da olabilir ...
        // Controller larda Post Put gibi metod action larında class daha uygun ...
        // isterseniz get isteği de olurdu ancak postisteği almak her zaman daha iyi  , RefreshToken body de taşınacağı için daha iyi ,get ile url de taşınmasın diye...
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshTokenAsync(refreshTokenDto.Token);

            return ActionResultInstance(result); // nocontent dönseydi daha uygun olurdu ... 200 dönüyor
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token);

            return ActionResultInstance(result);
        }
    }
}
