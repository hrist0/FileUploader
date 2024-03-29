using FileUploader.Data.Models;
using FileUploader.Data.Repositories;

namespace FileUploader.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<User> Login(string username, string password)
        {
            return await _loginRepository.Login(username, password);
        }

        public async Task<User> GetUser(string username)
        {
            return await _loginRepository.GetUser(username);
        }
    }
}
