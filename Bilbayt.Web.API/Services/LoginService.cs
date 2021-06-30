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
                var secret = _configuration.GetValue<string>("jwtSecret");
                var expiry = _configuration.GetValue<double>("jwtExpiry");
                var issuer = _configuration.GetValue<string>("baseUrl");

                result.Token = JWTokenCreator.GenerateAccessToken(new[] { new Claim("Id", user.Id) },secret ,expiry,issuer );

                return result;
            }

            result.Error = "Invalid username or password";

            return result;
        }
    }
}
