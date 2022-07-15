using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserFileController : CustomBaseController
    {
        private readonly IGenericService<UserFile, UserFileDto> _genericService;
        private readonly IUserFileService _userFileService;

        public UserFileController(IGenericService<UserFile, UserFileDto> genericService,IUserFileService userFileService)
        {
            _genericService = genericService;
            _userFileService = userFileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFiles()
        {             
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier); // userId

            return ActionResultInstance(await _userFileService.GetFilesAsync(userIdClaim.Value));
        }
    }
}
