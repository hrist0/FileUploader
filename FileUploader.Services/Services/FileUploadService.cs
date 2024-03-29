using FileUploader.Data.Models;
using FileUploader.Data.Repositories;
using FileUploader.Services.Services;
using Microsoft.AspNetCore.Http;

public class FileUploadService : IFileUploadService
{
    private readonly string _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
    private readonly IFileUploadRepository _fileUploadRepository;

    public FileUploadService(IFileUploadRepository fileUploadRepository)
    {
        _fileUploadRepository = fileUploadRepository;
    }

    public async Task<string> UploadFileAsync(IFormFile file, int userId)
    {
        try
        {
            if (!Directory.Exists(_uploadFolderPath))
                Directory.CreateDirectory(_uploadFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadFolderPath, fileName);

            var fileEntity = new UploadedFile
            {
                FileName = fileName,
                FilePath = filePath,
                UserId = userId
            };
            await _fileUploadRepository.UploadFile(fileEntity);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        catch
        {
            return null;
        }
    }

    public async Task<ICollection<UploadedFile>> GetUserFiles(int userId)
    {
        return await _fileUploadRepository.GetUserFiles(userId);
    }
}