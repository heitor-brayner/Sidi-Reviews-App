using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SidiReviews.ViewModel;

namespace SidiReviews.Views
{
    public sealed partial class MyReviewsPage : Page
    {
        public MyReviewsViewModel ViewModel { get; }

        public MyReviewsPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<MyReviewsViewModel>();                     
            this.DataContext = ViewModel;
        }
       
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.LoadUserReviewsCommand.ExecuteAsync(null);
        }
    }
}