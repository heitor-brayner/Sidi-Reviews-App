using SidiReviews.Interfaces;
using SidiReviews.Model;

namespace SidiReviews.Services
{
    public class SessionService : ISessionService
    {
        public User CurrentUser { get ; set ; }
    }
}
