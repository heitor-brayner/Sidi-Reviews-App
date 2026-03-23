using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SidiReviews.Interfaces;
using SidiReviews.Model;
using Microsoft.UI.Dispatching;

namespace SidiReviews.ViewModel
{
    public partial class MoviesViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;
        private readonly INavigatationService _navigationService;

        [ObservableProperty]
        private int _totalMoviesLoaded;

        [ObservableProperty]
        private string _lastErrorMessage = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasMovies))]
        private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();

        [ObservableProperty]
        private Movie? _selectedMovie;

        public bool HasMovies => Movies != null && Movies.Count > 0;

        public MoviesViewModel(IMovieService movieService, INavigatationService navigationService)
        {
            _movieService = movieService;
            _navigationService = navigationService;
            _ = LoadMovies();
        }

        partial void OnSelectedMovieChanged(Movie? value)
        {
            if (value != null)
            {
                NavigateToReviewsCore(value.Id);
            }
        }

        public async Task LoadMovies()
        {
            Movies.Clear();
            TotalMoviesLoaded = 0;

            try
            {
                var moviesList = await _movieService.GetAllMoviesAsync();
                foreach (var movie in moviesList)
                {
                    Movies.Add(movie);
                    TotalMoviesLoaded++;
                }
                OnPropertyChanged(nameof(HasMovies));
            }
            catch (Exception ex)
            {
                LastErrorMessage = "Failed to load movies.";
                System.Diagnostics.Debug.WriteLine($"Error loading movies: {ex.Message}");
            }
        }

        private void NavigateToReviewsCore(int movieId)
        {
            _navigationService.NavigateToReviews("ReviewsPage", movieId);

            var dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            if (dispatcherQueue != null)
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    SelectedMovie = null;
                });
            }
            else
            {
                SelectedMovie = null;
            }
        }
    }
}