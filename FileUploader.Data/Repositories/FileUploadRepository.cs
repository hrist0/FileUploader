using FileUploader.Data.Data;
using FileUploader.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Data.Repositories
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private AppDbContext _appDbContext;

        public FileUploadRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task UploadFile(UploadedFile file)
        {
            _appDbContext.Add(file);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<UploadedFile>> GetUserFiles(int userId)
        {
            return await _appDbContext.Files.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
