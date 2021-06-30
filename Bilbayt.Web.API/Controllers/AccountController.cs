using Bilbayt.Web.API.Dto.Request;
using Bilbayt.Web.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilbayt.Domain;

namespace Bilbayt.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;

        public AccountController(IUserService userService, ILoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var user = await _userService.GetUserByUsernameAsync(userDto.Username);

            if (user != null) return BadRequest("User already exist");

            user = new ApplicationUser
            {
                Username = userDto.Username,
                Password = userDto.Password,
                FullName = userDto.FullName
            };

            await _userService.CreateUser(user);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetUserByUsernameAsync(loginDto.Username);

            if (user == null) return BadRequest("Invalid username or password");

            var identityResult = await _loginService.SignIn(user, loginDto.Password);

            if(!identityResult.IsSuccess) return BadRequest(identityResult.Error);

            return Ok(new { token = identityResult.Token });
        }
    }
}
