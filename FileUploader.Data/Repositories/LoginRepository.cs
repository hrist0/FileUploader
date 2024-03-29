using FileUploader.Data.Data;
using FileUploader.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private AppDbContext _appDbContext;

        public LoginRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };

            _appDbContext.Add(user);
            await _appDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUser(string username)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(e => e.Username == username);
        }
    }
}
