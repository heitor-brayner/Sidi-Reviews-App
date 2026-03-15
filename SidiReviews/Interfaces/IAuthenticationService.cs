using System.Threading.Tasks;
using SidiReviews.Model;

namespace SidiReviews.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateUserAsync(string userName, string password);
        Task<string?> RegisterUserAync(string userName, string email, string password);
        Task LogoutUserAsync();
    }
}
