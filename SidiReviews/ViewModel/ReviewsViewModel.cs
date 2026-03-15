using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SidiReviews.Interfaces; 
using SidiReviews.Model;
using System.Collections.ObjectModel; 
using System.Linq; 
using System.Threading.Tasks;
using System;
using SidiReviews.Helpers;

namespace SidiReviews.ViewModel
{
    public partial class ReviewsViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;
        private readonly INavigatationService _navigationService;
        private readonly IReviewsService _reviewService;
        private readonly ISessionService _sessionService;

        [ObservableProperty]
        private Movie? _currentMovie;

        [ObservableProperty]
        private ObservableCollection<Review> _reviews = new ObservableCollection<Review>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasReviews))]
        private double _averageRating;

        public bool HasReviews => Reviews.Any();

        public ReviewsViewModel(IMovieService movieService, INavigatationService navigationService, 
                                IReviewsService reviewService, ISessionService sessionService)     
        {
            _movieService = movieService;
            _navigationService = navigationService; 
            _reviewService = reviewService;       
            _sessionService = sessionService;     
        }

        public async Task LoadMovieAsync(int movieId)
        {
            Reviews.Clear();      
            CurrentMovie = null; 
            AverageRating = 0;    

            try
            {
                CurrentMovie = await _movieService.GetMovieByIdAsync(movieId);

                if (CurrentMovie != null)
                {
                    var reviewList = await _reviewService.GetReviewsForMovieAsync(movieId);
                    if (reviewList != null)
                    {
                        foreach (var review in reviewList)
                        {
                            Reviews.Add(review); 
                        }
                        CalculateAverageRating();
                        OnPropertyChanged(nameof(HasReviews));
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Erro: Filme com ID {movieId} não encontrado.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar dados para ReviewsPage: {ex.Message}");
            }
        }

        private void CalculateAverageRating()
        {
            if (Reviews.Any())
            {
                AverageRating = Reviews.Average(r => r.Rating);
            }
            else
            {
                AverageRating = 0;
            }
        }

        [RelayCommand(CanExecute = nameof(CanNavigateToAddReview))]
        private void NavigateToAddReview()
        {
            if (CurrentMovie != null)
            {
                _navigationService.NavigateToReviews("ReviewFormPage", new ReviewFormNavigationParameter(movieId: this._currentMovie.Id));
            }
        }

        private bool CanNavigateToAddReview()
        {
            return _sessionService.CurrentUser != null && CurrentMovie != null;
        }
    }
}