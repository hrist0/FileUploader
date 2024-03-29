using FileUploader.Controllers;
using FileUploader.Data.Models;
using FileUploader.Services.Services;
using FileUploader.Web.ErorrMessages;
using FileUploader.Web.JwtHelper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FileUploader.Tests.Controllers
{
    public class LoginControllerTests
    {
        [Fact]
        public async Task Login_WithValidRequest_ReturnsToken()
        {
            // Arrange
            var loginServiceMock = new Mock<ILoginService>();
            loginServiceMock.Setup(service => service.GetUser(It.IsAny<string>())).ReturnsAsync(new User { Id = 1 });
            loginServiceMock.Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new User { Id = 1 });

            var jwtTokenServiceMock = new Mock<IJwtTokenService>();
            jwtTokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<int>())).Returns("mocked_token");

            var controller = new LoginController(loginServiceMock.Object, jwtTokenServiceMock.Object);
            var loginRequest = new LoginRequest { Email = "test@example.com", Password = "password" };

            // Act
            var result = await controller.Login(loginRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("mocked_token", result.Value);
        }

        [Fact]
        public async Task Login_WithInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var controller = new LoginController(null, null);
            controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = await controller.Login(null) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}