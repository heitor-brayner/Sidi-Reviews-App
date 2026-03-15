using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SidiReviews.Data;
using SidiReviews.Interfaces;
using SidiReviews.Model;

namespace SidiReviews.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _dbContext;
        private readonly ISessionService _sessionService;
        public AuthenticationService(AppDbContext dbContext, ISessionService sessionService)
        {
            _dbContext = dbContext;
            _sessionService = sessionService;
        }
        public async Task<User> AuthenticateUserAsync(string userName, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password)) 
            {
                _sessionService.CurrentUser = user;
                return user;
            }
            return null;
        }
        public async Task<string?> RegisterUserAync(string userName, string email, string password)
        {

            if (await _dbContext.Users.AnyAsync(u => u.UserName == userName))
            {
                return "O nome de usuário já está em uso.";
            }
            if (await _dbContext.Users.AnyAsync(u => u.Email == email))
            {
                return "O email já está em uso.";
            }

            var newUser = new User
            {
                UserName = userName,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return null;
        }

        public Task LogoutUserAsync()
        {
            var usuariologado = _sessionService.CurrentUser;
            _sessionService.CurrentUser = null;
            return Task.CompletedTask;
        }

    }
}
