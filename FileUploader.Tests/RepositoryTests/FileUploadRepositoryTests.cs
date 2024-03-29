using FileUploader.Data.Models;
using FileUploader.Data.Repositories;
using FileUploader.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FileUploader.Tests.RepositoryTests
{
    public class FileUploadRepositoryTests
    {
        [Fact]
        public async Task UploadFile_ValidFile_SavesFile()
        {
            // Arrange
            var file = new Mock<IFormFile>();
            var userId = 1;
            var expectedFileName = "test.jpg";
            var repositoryMock = new Mock<IFileUploadRepository>();
            repositoryMock.Setup(repo => repo.UploadFile(It.IsAny<UploadedFile>()))
                          .Returns(Task.CompletedTask);
            var service = new FileUploadService(repositoryMock.Object);

            // Act
            var result = await service.UploadFileAsync(file.Object, userId);

            // Assert
            repositoryMock.Verify(repo => repo.UploadFile(It.IsAny<UploadedFile>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserFiles_ReturnsUserFiles()
        {
            // Arrange
            var userId = 1;
            var expectedFiles = new List<UploadedFile>
            {
                new UploadedFile { FileName = "file1.jpg", FilePath = "path1", UserId = userId },
                new UploadedFile { FileName = "file2.pdf", FilePath = "path2", UserId = userId }
            };
            var repositoryMock = new Mock<IFileUploadRepository>();
            repositoryMock.Setup(repo => repo.GetUserFiles(userId))
                          .ReturnsAsync(expectedFiles);
            var service = new FileUploadService(repositoryMock.Object);

            // Act
            var result = await service.GetUserFiles(userId);

            // Assert
            repositoryMock.Verify(repo => repo.GetUserFiles(userId), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(expectedFiles.Count, result.Count);
            Assert.Equal(expectedFiles, result);
        }
    }
}