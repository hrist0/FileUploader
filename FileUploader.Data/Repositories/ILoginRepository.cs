using FileUploader.Data.Models;

namespace FileUploader.Data.Repositories
{
    public interface ILoginRepository
    {
        Task<User> Login(string username, string password);

        public Task<User> GetUser(string username);
    }
}
