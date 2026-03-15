using SidiReviews.Model;

namespace SidiReviews.Interfaces
{
    public interface ISessionService
    {
        public User CurrentUser { get; set; }
    }
}
