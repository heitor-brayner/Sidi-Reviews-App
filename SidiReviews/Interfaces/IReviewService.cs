using System.Collections.Generic;
using System.Threading.Tasks;
using SidiReviews.Model;

namespace SidiReviews.Interfaces
{
    public interface IReviewsService
    {
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<List<Review>> GetAllReviewsAsync();
        Task<Review> AddReviewAsync(Review review);
        Task<bool> UpdateReviewAsync(Review review);
        Task<Review> RemoveReviewAsync(int reviewId);
        Task<IEnumerable<Review>> GetReviewsForMovieAsync(int movieId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId);

    }
}
