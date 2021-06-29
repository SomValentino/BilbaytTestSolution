using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bilbayt.Domain;
using Bilbayt.Web.API.Models;
using Bilbayt.Web.API.Services.Interfaces;
using Bilbayt.Web.API.utils;
using Microsoft.Extensions.Configuration;

namespace Bilbayt.Web.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IdentityResult> SignIn(ApplicationUser user, string password)
        {
            var result = new IdentityResult();

            result.IsSuccess = user.Password.compare(password);

            if (result.IsSuccess)
            {
                // Todo: get JWT token
                result.Token = JWTokenCreator.GenerateAccessToken(new[] { new Claim(ClaimTypes.PrimarySid, user.Id) },
                    _configuration.GetValue<string>("jwtSecret"), _configuration.GetValue<double>("jwtExpiry"));

                return result;
            }

            result.Error = "Invalid username or password";

            return result;
        }
    }
}
