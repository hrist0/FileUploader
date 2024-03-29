using FileUploader.Services.Services;
using FileUploader.Web.ErorrMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileUploader.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly string _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var userId = GetUserIdFromToken();
            if (!userId.HasValue)
                return Unauthorized(ErrorMessages.InvalidToken);

            try
            {
                // Validate file
                var validationResult = ValidateFile(file);
                if (validationResult != null)
                    return validationResult;

                // Save the file to the server
                var result = await _fileUploadService.UploadFileAsync(file, userId.Value);
                if (result == null)
                    return BadRequest(ErrorMessages.FailedToUploadFile);

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(ErrorMessages.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFiles()
        {
            var userId = GetUserIdFromToken();
            if (!userId.HasValue)
                return Unauthorized(ErrorMessages.InvalidToken);
            
            var userFiles = await _fileUploadService.GetUserFiles(userId.Value);
            return Ok(userFiles);
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var userId = GetUserIdFromToken();
            if (!userId.HasValue)
                return Unauthorized(ErrorMessages.InvalidToken);

            // Check if the file exists in the upload folder
            var filePath = Path.Combine(_uploadFolderPath, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            // Check if the file belongs to the authenticated user
            var userFiles = await _fileUploadService.GetUserFiles(userId.Value);
            if (!userFiles.Any(f => f.FileName == fileName && f.UserId == userId))
                return Forbid(); // User does not have permission to download the file

            // Return the file as a FileStreamResult
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/octet-stream", fileName);
        }

        private int? GetUserIdFromToken()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                return userId;

            return null;
        }

        private IActionResult ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(ErrorMessages.NoFileUploaded);

            // Validate file size
            if (file.Length > 5242880) // 5MB limit
                return BadRequest(ErrorMessages.FileSizeExceedsLimit);

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BadRequest(ErrorMessages.InvalidFileType);

            return null;
        }
    }
}
