using Core.Dtos.Users;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return CreateActionResult(await _userService.CreateUserAsync(createUserDto));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser(string username)
        {
            return CreateActionResult(await _userService.GetUserByNameAsync(username));
        }

        [HttpPost("CreateUserRoles/{userName}")]
        public async Task<IActionResult> CreateUserRoles(string userName)
        {
            return CreateActionResult(await _userService.CreateUserRoles(userName));
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(ResetPasswordDto resetPasswordDto)
        {
            return CreateActionResult(await _userService.UpdatePassword(resetPasswordDto));
        }
    }
}
