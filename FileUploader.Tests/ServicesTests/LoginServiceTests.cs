using FileUploader.Data.Models;
using FileUploader.Data.Repositories;
using FileUploader.Services.Services;
using Moq;

namespace FileUploader.Tests.ServicesTests
{
    public class LoginServiceTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var loginRepositoryMock = new Mock<ILoginRepository>();
            var service = new LoginService(loginRepositoryMock.Object);
            var username = "testuser";
            var password = "password";
            var expectedUser = new User { Username = username, Password = password };
            loginRepositoryMock.Setup(repo => repo.Login(username, password)).ReturnsAsync(expectedUser);

            // Act
            var result = await service.Login(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task GetUser_ExistingUser_ReturnsUser()
        {
            // Arrange
            var loginRepositoryMock = new Mock<ILoginRepository>();
            var service = new LoginService(loginRepositoryMock.Object);
            var username = "existinguser";
            loginRepositoryMock.Setup(repo => repo.GetUser(username)).ReturnsAsync(new User { Id = 1 });

            // Act
            var result = await service.GetUser(username);

            // Assert
            Assert.NotNull(result);
        }
    }
}
