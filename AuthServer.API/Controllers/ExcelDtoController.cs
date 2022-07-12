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
    public class ExcelDtoController : CustomBaseController
    {

        private readonly IGenericService<UserFile, UserFileDto> _genericService;
        private readonly IExcelDtoService _excelDtoService;
        private readonly UserManager<UserApp> _userManager;

        public ExcelDtoController(IGenericService<UserFile, UserFileDto> genericService,IExcelDtoService excelDtoService, UserManager<UserApp> userManager)
        {
            _genericService = genericService;
            _excelDtoService = excelDtoService;
            _userManager = userManager;
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetFiles(string userId)
        {
            //return ActionResultInstance(await _excelDtoService.GetFilesAsync(HttpContext.User.Identity.Name));
            return ActionResultInstance(await _excelDtoService.GetFilesAsync(userId));

        }
    }
}
