using Bilbayt.Web.API.Dto.Request;
using Bilbayt.Web.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilbayt.Domain;
using Bilbayt.Web.API.Dto.Response;

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

            if (user != null) return BadRequest(new ErrorDto { Errors = "User already exist" });

            var userEmail = await _userService.GetUserByEmailAsync(userDto.Email);

            if (userEmail != null) return BadRequest(new ErrorDto { Errors = "User already exist with same email" });

            user = new ApplicationUser
            {
                Username = userDto.Username,
                Password = userDto.Password,
                FullName = userDto.FullName,
                Email = userDto.Email
            };

            await _userService.CreateUser(user);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetUserByUsernameAsync(loginDto.Username);

            if (user == null) return BadRequest(new ErrorDto { Errors = "Invalid username or password" });

            var identityResult = await _loginService.SignIn(user, loginDto.Password);

            if(!identityResult.IsSuccess) return BadRequest( new ErrorDto { Errors = identityResult.Error });

            return Ok(new TokenDto { Token = identityResult.Token });
        }
    }
}
