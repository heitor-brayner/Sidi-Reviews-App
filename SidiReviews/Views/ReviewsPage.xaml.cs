using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SidiReviews.ViewModel; 

namespace SidiReviews.Views
{
    public sealed partial class ReviewsPage : Page
    {
        public ReviewsViewModel ViewModel { get; }

        public ReviewsPage()
        {
            this.InitializeComponent();

            var viewModel = Ioc.Default.GetRequiredService<ReviewsViewModel>();

            ViewModel = viewModel;
            this.DataContext = ViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            if (e.Parameter is int movieId && movieId > 0)
            {

                await ViewModel.LoadMovieAsync(movieId);

            }

        }
    }
}