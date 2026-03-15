using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SidiReviews.Data;
using SidiReviews.Interfaces;
using SidiReviews.Model;

namespace SidiReviews.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _dbContext;
        public MovieService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }
        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _dbContext.Movies.FindAsync(movieId);
        }
        public async Task<Movie> AddMovieAsync(Movie movie) 
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }
        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            _dbContext.Update(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }
        public async Task<Movie> DeleteMovieAsync(int movieId)
        {
            var movie = await _dbContext.Movies.FindAsync(movieId);
            if (movie != null) 
            {
                _dbContext.Movies.Remove(movie);
                await _dbContext.SaveChangesAsync();
            }
            return movie;
        }
    }
}
