using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using SidiReviews.Data;
using SidiReviews.Interfaces;
using SidiReviews.Model;

namespace SidiReviews.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly AppDbContext _dbContext;
        public ReviewsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _dbContext.Reviews.FindAsync(reviewId);
        }
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _dbContext.Reviews.ToListAsync();
        }
        public async Task<Review> AddReviewAsync(Review review)
        {
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
        public async Task<bool> UpdateReviewAsync(Review reviewFromViewModel)
        {
            if (reviewFromViewModel == null || reviewFromViewModel.Id <= 0)
            {
                return false;
            }

            var existingReview = await _dbContext.Reviews.FindAsync(reviewFromViewModel.Id);

            if (existingReview == null)
            {
                return false;
            }

            existingReview.Content = reviewFromViewModel.Content;
            existingReview.Rating = reviewFromViewModel.Rating;

            int affectedRows = await _dbContext.SaveChangesAsync();

            return affectedRows > 0;

        }

        public async Task<Review> RemoveReviewAsync(int reviewId)
        {
            var review = await _dbContext.Reviews.FindAsync(reviewId);
            if(review != null)
            {
                _dbContext.Reviews.Remove(review);
                await _dbContext.SaveChangesAsync();    
            }
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsForMovieAsync(int movieId)
        {
            try
            {
                var reviews = await _dbContext.Reviews
                                            .Where(r => r.MovieId == movieId)
                                            .Include(r => r.User)
                                            .OrderByDescending(r => r.CreationDate)
                                            .ToListAsync();
                return reviews;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao buscar reviews para MovieId {movieId}: {ex.Message}");
                return Enumerable.Empty<Review>();
            }
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId)
        {
            try
            {
                // Busca reviews pelo UserId, incluindo o Movie associado
                // MUITO IMPORTANTE: .Include(r => r.Movie) para ter acesso à imagem/título do filme
                var reviews = await _dbContext.Reviews
                                            .Where(r => r.UserId == userId)
        .Include(r => r.Movies) // Inclui dados do Filme!!
                                            .OrderByDescending(r => r.CreationDate) // Ordena
                                            .ToListAsync();
                return reviews;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao buscar reviews do usuário {userId}: {ex.Message}");
                return Enumerable.Empty<Review>();
            }
        }
    }
}
