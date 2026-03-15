using System.Collections.Generic;
using System.Threading.Tasks;
using SidiReviews.Model;

namespace SidiReviews.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task<Movie> AddMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(Movie movie);
        Task<Movie> DeleteMovieAsync(int movieId);
    }
}
