using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserFileController : CustomBaseController
    {
        private readonly IGenericService<UserFile, UserFileDto> _genericService;
        private readonly IUserFileService _userFileService;
        private readonly UserManager<UserApp> _userManager;

        public UserFileController(IGenericService<UserFile, UserFileDto> genericService,IUserFileService userFileService, UserManager<UserApp> userManager)
        {
            _genericService = genericService;
            _userFileService = userFileService;
            _userManager = userManager;
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetFiles(string userId)
        {
            //return ActionResultInstance(await _userFileService.GetFilesAsync(HttpContext.User.Identity.Name));
            return ActionResultInstance(await _userFileService.GetFilesAsync(userId));

        }
    }
}
