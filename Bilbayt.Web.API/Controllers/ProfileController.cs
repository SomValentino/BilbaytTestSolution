using Bilbayt.Web.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid user identifier");

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null) return NotFound("User profile not found");


            return Ok(new { Username = user.Username, FullName = user.FullName });
        }
    }
}
