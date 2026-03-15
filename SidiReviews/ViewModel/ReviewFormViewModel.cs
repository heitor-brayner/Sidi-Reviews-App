using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SidiReviews.Interfaces;
using SidiReviews.Model;
using System;
using System.Threading.Tasks;
using SidiReviews.Helpers;

namespace SidiReviews.ViewModel
{
    public partial class ReviewFormViewModel : ObservableObject
    {
        private readonly IReviewsService _reviewService;
        private readonly INavigatationService _navigationService;
        private readonly ISessionService _sessionService;
        private readonly IMovieService _movieService; 

        private bool _isEditMode = false;
        private int? _editingReviewId = null;
        private int _movieIdForNavigation;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveOrUpdateCommand))]
        private string? _reviewContent; 

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveOrUpdateCommand))]
        private double _rating;


        [ObservableProperty]
        private string? _movieTitle;

        [ObservableProperty]
        private string? _movieImagePath;

        public string PageTitle => _isEditMode ? $"Editar Review para {MovieTitle}" : $"Sua Review para {MovieTitle}";
        public string SaveButtonText => _isEditMode ? "Atualizar Review" : "Salvar Review";

        public ReviewFormViewModel(IReviewsService reviewService, INavigatationService navigationService, ISessionService sessionService, IMovieService movieService)
        {
            _reviewService = reviewService;
            _navigationService = navigationService;
            _sessionService = sessionService;
            _movieService = movieService; 
        }

        public async Task InitializeOrLoadAsync(ReviewFormNavigationParameter parameter)
        {
            ResetForm();
            Movie? movieData = null;

            if (parameter.ReviewId.HasValue) 
            {
                _isEditMode = true;
                _editingReviewId = parameter.ReviewId.Value;
                var existingReview = await _reviewService.GetReviewByIdAsync(_editingReviewId.Value); 
                if (existingReview != null)
                {
                    ReviewContent = existingReview.Content;
                    Rating = existingReview.Rating;
                    _movieIdForNavigation = existingReview.MovieId;
                    movieData = existingReview.Movies; 
                }
            }
            else if (parameter.MovieId.HasValue)
            {
                _isEditMode = false;
                _editingReviewId = null;
                _movieIdForNavigation = parameter.MovieId.Value;
                movieData = await _movieService.GetMovieByIdAsync(_movieIdForNavigation);
            }

            if (movieData != null)
            {
                MovieTitle = movieData.Title;
                MovieImagePath = movieData.ImagePath; 
            }
            else
            {
                MovieTitle = "Filme Desconhecido";
                MovieImagePath = null; 
            }

            OnPropertyChanged(nameof(PageTitle));
            OnPropertyChanged(nameof(SaveButtonText));
            SaveOrUpdateCommand.NotifyCanExecuteChanged();
        }

        private void ResetForm()
        {
            ReviewContent = string.Empty;
            Rating = 0;
            _isEditMode = false;
            _editingReviewId = null;
            _movieIdForNavigation = -1;
            MovieTitle = null;
            MovieImagePath = null;
        }

        private bool CanSaveOrUpdate() 
        {
            return _sessionService.CurrentUser != null && Rating > 0 && !string.IsNullOrWhiteSpace(ReviewContent) && _movieIdForNavigation > 0;
        }

        [RelayCommand(CanExecute = nameof(CanSaveOrUpdate))]
        private async Task SaveOrUpdateAsync()
        {
            SaveOrUpdateCommand.NotifyCanExecuteChanged();
            string operationType = _isEditMode ? "atualizar" : "salvar";

            try
            {
                int? userId = _sessionService.CurrentUser?.Id;

                if (_isEditMode && _editingReviewId.HasValue) 
                {
                    var reviewToUpdate = new Review
                    {
                        Id = _editingReviewId.Value,
                        MovieId = _movieIdForNavigation,
                        UserId = userId.Value,
                        Content = this.ReviewContent ?? string.Empty,
                        Rating = (int)Math.Round(this.Rating),
                    };
                    var success = await _reviewService.UpdateReviewAsync(reviewToUpdate);
                    if (success)
                    {
                        _navigationService.NavigateToReviews("ReviewsPage", reviewToUpdate.MovieId);
                    }
                }
                else if (!_isEditMode && _movieIdForNavigation > 0) 
                {
                    var newReview = new Review
                    {
                        MovieId = _movieIdForNavigation,
                        UserId = userId.Value,
                        Content = this.ReviewContent ?? string.Empty,
                        Rating = (int)Math.Round(this.Rating),
                        CreationDate = DateTime.UtcNow
                    };
                    await _reviewService.AddReviewAsync(newReview);
                    _navigationService.NavigateToReviews("ReviewsPage", newReview.MovieId);

                }

            }
            catch (Exception ex) 
            { 
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar a review: {ex.Message}");
            }
            finally
            { 
                SaveOrUpdateCommand.NotifyCanExecuteChanged();
            }
        }

    }
}