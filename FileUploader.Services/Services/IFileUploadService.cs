using FileUploader.Data.Models;
using Microsoft.AspNetCore.Http;

namespace FileUploader.Services.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, int userId);

        Task<ICollection<UploadedFile>> GetUserFiles(int userId);
    }
}
