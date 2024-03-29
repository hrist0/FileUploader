using FileUploader.Data.Models;

namespace FileUploader.Data.Repositories
{
    public interface IFileUploadRepository
    {
        Task UploadFile(UploadedFile file);

        Task<ICollection<UploadedFile>> GetUserFiles(int userId);
    }
}
