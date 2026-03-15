using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SidiReviews.Interfaces;
using SidiReviews.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.UI.Xaml.Controls;
using SidiReviews.Helpers;


namespace SidiReviews.ViewModel
{
    public partial class MyReviewsViewModel : ObservableObject
    {
        private readonly IReviewsService _reviewService;
        private readonly ISessionService _sessionService;
        private readonly INavigatationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Review> _userReviews = new ObservableCollection<Review>();

        public bool HasReviews => UserReviews.Any();

        public MyReviewsViewModel(IReviewsService reviewService, ISessionService sessionService, INavigatationService navigationService)
        {
            _reviewService = reviewService;
            _sessionService = sessionService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        public async Task LoadUserReviewsAsync()
        {
            int? userId = _sessionService.CurrentUser?.Id;
            if (!userId.HasValue)
            {
                UserReviews.Clear();
                OnPropertyChanged(nameof(HasReviews));
                return;
            }

            UserReviews.Clear();

            try
            {
                var reviews = await _reviewService.GetReviewsByUserIdAsync(userId.Value);
                if (reviews != null)
                {
                    foreach (var review in reviews)
                    {
                        UserReviews.Add(review);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar minhas reviews: {ex.Message}");
            }
            finally
            {
                OnPropertyChanged(nameof(HasReviews));
            }
        }

        [RelayCommand]
        private void GoToEditReview(Review? reviewToEdit)
        {
            if (reviewToEdit != null)
            {
                _navigationService.NavigateToReviews("ReviewFormPage", new ReviewFormNavigationParameter(reviewId: reviewToEdit.Id));
            }
        }

        [RelayCommand]
        private async Task DeleteReviewAsync(Review? reviewToDelete)
        {
            if (reviewToDelete == null) return;


            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Confirmar Exclusão",
                Content = $"Tem certeza que deseja excluir sua review para o filme '{reviewToDelete.Movies?.Title ?? "Desconhecido"}'?",
                PrimaryButtonText = "Excluir",
                CloseButtonText = "Cancelar",
                XamlRoot = App.m_window.Content.XamlRoot
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                System.Diagnostics.Debug.WriteLine($"Usuário confirmou exclusão da Review ID: {reviewToDelete.Id}");

                var response = await _reviewService.RemoveReviewAsync(reviewToDelete.Id);
                if (response != null)
                {
                    UserReviews.Remove(reviewToDelete);
                }
                OnPropertyChanged(nameof(HasReviews));
                _navigationService.NavigateToReviews("ReviewsPage", reviewToDelete.MovieId);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Exclusão cancelada pelo usuário.");

            }
        }
    }
}