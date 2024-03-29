using FileUploader.Data.Models;
using FileUploader.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using FileUploader.Data.Data;

namespace FileUploader.Tests.RepositoryTests
{
    public class LoginRepositoryTests
    {
        [Fact]
        public async Task Login_ValidCredentials_SavesUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;
            using var dbContext = new AppDbContext();
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.EnsureCreatedAsync();
            var repository = new LoginRepository(dbContext);
            var username = "testuser";
            var password = "password";

            // Act
            var result = await repository.Login(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            Assert.Equal(password, result.Password);

            // Check if user is saved in the database
            var savedUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            Assert.NotNull(savedUser);
            Assert.Equal(username, savedUser.Username);
            Assert.Equal(password, savedUser.Password);
        }

        [Fact]
        public async Task GetUser_ExistingUser_ReturnsUser()
        {
            // Arrange
            using var dbContext = new AppDbContext();
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.EnsureCreatedAsync();
            var repository = new LoginRepository(dbContext);
            var existingUsername = "existinguser";
            dbContext.Users.Add(new User { Username = existingUsername, Password = "password" });
            await dbContext.SaveChangesAsync();

            // Act
            var result = await repository.GetUser(existingUsername);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUser_NonExistingUser_ReturnsNull()
        {
            // Arrange
            using var dbContext = new AppDbContext();
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.EnsureCreatedAsync();
            var repository = new LoginRepository(dbContext);
            var nonExistingUsername = "nonexistinguser";

            // Act
            var result = await repository.GetUser(nonExistingUsername);

            // Assert
            Assert.Null(result);
        }
    }
}
