using FileUploader.Data.Models;

namespace FileUploader.Services.Services
{
    public interface ILoginService
    {
        Task<User> Login(string username, string password);

        Task<User> GetUser(string username);
    }
}
