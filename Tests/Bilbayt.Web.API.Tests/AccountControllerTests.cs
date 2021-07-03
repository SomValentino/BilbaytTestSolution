using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bilbayt.Web.API.Controllers;
using Bilbayt.Web.API.Dto.Response;
using Bilbayt.Web.API.Services;
using Bilbayt.Web.API.Services.Interfaces;
using Bilbayt.Web.API.Tests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Bilbayt.Web.API.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly IConfiguration _configuration;
        private readonly LoginService _loginService;

        public AccountControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            TestMocks.SetupUserServiceMock(_userServiceMock);

            _configuration = TestMocks.SetupIConfiguration();

            _loginService = new LoginService(_configuration);
        }

        [Fact]
        public async Task Login_RightCredentials_ReturnJWToken()
        {
            var accountController = new AccountController(_userServiceMock.Object, _loginService);

            var result = await accountController.Login(new Dto.Request.LoginDto
            {
                Username = "testam",
                Password = "Test@3456@#"
            });

            var okResult = result as OkObjectResult;

            var response = okResult.Value as TokenDto;

            var statusCode = 200;

            Assert.Equal(okResult.StatusCode, statusCode);
            Assert.NotNull(response);
            Assert.NotEmpty(response.Token);
        }

        [Fact]
        public async Task Login_WrongUsername_ReturnsBadRequest()
        {
            var accountController = new AccountController(_userServiceMock.Object, _loginService);

            var result = await accountController.Login(new Dto.Request.LoginDto
            {
                Username = "testam@######",
                Password = "Test@3456@#"
            });

            var badRequestObject = result as BadRequestObjectResult;

            var statusCode = 400;

            var response = badRequestObject.Value as ErrorDto;

            var messageExpected = "Invalid username or password";

            Assert.Equal(badRequestObject.StatusCode, statusCode);
            Assert.NotNull(response);
            Assert.Equal(response.Errors,messageExpected);
        }

        [Fact]
        public async Task Login_WrongPassword_ReturnsBadRequest()
        {
            var accountController = new AccountController(_userServiceMock.Object, _loginService);

            var result = await accountController.Login(new Dto.Request.LoginDto
            {
                Username = "testam",
                Password = "Test@3456@#mmmeretey"
            });

            var badRequestObject = result as BadRequestObjectResult;

            var statusCode = 400;

            var response = badRequestObject.Value as ErrorDto;

            var messageExpected = "Invalid username or password";

            Assert.Equal(badRequestObject.StatusCode, statusCode);
            Assert.NotNull(response);
            Assert.Equal(response.Errors, messageExpected);
        }
    }
}
