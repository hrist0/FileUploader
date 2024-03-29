using FileUploader.Controllers;
using FileUploader.Data.Models;
using FileUploader.Services.Services;
using FileUploader.Web.ErorrMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace FileUploader.Tests.ContollersTests
{
    public class FileUploadControllerTests
    {
        [Fact]
        public async Task UploadFile_ValidFile_ReturnsOkResult()
        {
            // Arrange
            var fileUploadServiceMock = new Mock<IFileUploadService>();
            var controller = new FileUploadController(fileUploadServiceMock.Object);
            SetupUser(controller);

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(1024); // Set file size to 1KB
            formFileMock.Setup(f => f.FileName).Returns("test.jpg"); // Set file name with extension

            // Set up expectations for the UploadFileAsync method to return a non-null result
            var expectedResult = "fileName.jpg";
            fileUploadServiceMock.Setup(s => s.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<int>())).ReturnsAsync(expectedResult);

            // Act
            var result = await controller.UploadFile(formFileMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("File uploaded successfully.", okResult.Value);
        }

        [Fact]
        public async Task UploadFile_InvalidFile_ReturnsBadRequest()
        {
            // Arrange
            var fileUploadServiceMock = new Mock<IFileUploadService>();
            var controller = new FileUploadController(fileUploadServiceMock.Object);
            SetupUser(controller);
            var formFile = new Mock<IFormFile>();
            formFile.Setup(f => f.Length).Returns(0); // Set file size to 0

            // Act
            var result = await controller.UploadFile(formFile.Object);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.NoFileUploaded, badRequestResult.Value);
        }

        [Fact]
        public async Task GetUserFiles_AuthorizedUser_ReturnsUserFiles()
        {
            // Arrange
            var expectedUserFiles = new List<UploadedFile> { new UploadedFile { FileName = "file1.jpg" }, new UploadedFile { FileName = "file2.pdf" } };
            var userId = 1;
            var fileUploadServiceMock = new Mock<IFileUploadService>();
            var controller = new FileUploadController(fileUploadServiceMock.Object);
            SetupUser(controller);
            fileUploadServiceMock.Setup(s => s.GetUserFiles(userId)).ReturnsAsync(expectedUserFiles);

            // Act
            var result = await controller.GetUserFiles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userFiles = Assert.IsAssignableFrom<IEnumerable<UploadedFile>>(okResult.Value);
            Assert.Equal(expectedUserFiles, userFiles);
        }

        [Fact]
        public async Task DownloadFile_FileDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var fileName = "testFile.jpg"; // Example file name
            var fileUploadServiceMock = new Mock<IFileUploadService>();
            var controller = new FileUploadController(fileUploadServiceMock.Object);
            SetupUser(controller);

            // Act
            var result = await controller.DownloadFile(fileName);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private static void SetupUser(FileUploadController controller)
        {
            var userId = 1;

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }
    }
}
