using System;
using System.Collections.Generic;
using Bilbayt.Web.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Bilbayt.Web.API.Tests.Setup
{
    public class TestMocks
    {
        public static void SetupUserServiceMock(Mock<IUserService> mock)
        {
            mock.Setup(x => x.GetUserByUsernameAsync("testam")).ReturnsAsync(TestApplicationUser.GetApplicationUser());
            mock.Setup(x => x.GetUserByEmailAsync("test@gmail.com")).ReturnsAsync(TestApplicationUser.GetApplicationUser());
        }

        public static IConfiguration SetupIConfiguration()
        {
            return new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                {"jwtSecret", TestApplicationUser.GetJWTSecret() },
                {"jwtExpiry", TestApplicationUser.GetJWTExpry().ToString() },
                {"baseUrl", TestApplicationUser.BaseUrl() }
            }).Build();
        }
    }
}
