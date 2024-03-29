using FileUploader.Data.Models;
using FileUploader.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FileUploader.Tests.ServicesTests
{
    public class FileUploadServiceTests
    {
        [Fact]
        public async Task UploadFileAsync_ValidFile_ReturnsFileName()
        {
            // Arrange
            var fileUploadRepositoryMock = new Mock<IFileUploadRepository>();
            var service = new FileUploadService(fileUploadRepositoryMock.Object);
            var fileMock = new Mock<IFormFile>();
            var userId = 1;

            // Act
            var result = await service.UploadFileAsync(fileMock.Object, userId);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetUserFiles_ValidUserId_ReturnsUserFiles()
        {
            // Arrange
            var fileUploadRepositoryMock = new Mock<IFileUploadRepository>();
            var service = new FileUploadService(fileUploadRepositoryMock.Object);
            var userId = 1;
            var expectedFiles = new List<UploadedFile>
            {
            new UploadedFile { FileName = "file1.jpg", FilePath = "path1", UserId = userId },
            new UploadedFile { FileName = "file2.pdf", FilePath = "path2", UserId = userId }
            };
            fileUploadRepositoryMock.Setup(repo => repo.GetUserFiles(userId)).ReturnsAsync(expectedFiles);

            // Act
            var result = await service.GetUserFiles(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedFiles, result);
        }
    }
}
