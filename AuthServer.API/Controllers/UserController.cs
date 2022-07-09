using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            //throw new CustomException("Veritabanı ile ilgili bir hata meydana geldi");
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }



        //Action içerisinde Arka arkaya kullanılan code tekrarı varsa attribute yapıp kullanıyor... action içleri temiz olsun,bussiness işlerini servis katmanında yapmak daha iyi ......


        [Authorize] // endpoint'in token istemesi //program.cs de  app.UseAuthentication(); //şimdilik sadece burada token isteyen [Authorize] ihtiyaç duyuldu ...
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            // TokenService teki  new Claim(ClaimTypes.Name,userApp.UserName) ile ...
            // aksi halde HttpContext.User.Claims.Where(x=>x.Type=="username").FirstOrDefault diyerek bulmaya çalışılacaktık ...

            return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
    }
}
