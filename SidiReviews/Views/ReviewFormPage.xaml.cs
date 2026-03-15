using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SidiReviews.ViewModel;
using SidiReviews.Helpers;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SidiReviews.Views
{
    public sealed partial class ReviewFormPage : Page
    {
        public ReviewFormViewModel ViewModel { get; }

        public ReviewFormPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<ReviewFormViewModel>();
            this.DataContext = ViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is ReviewFormNavigationParameter navParameter)
            {
                await ViewModel.InitializeOrLoadAsync(navParameter);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ReviewFormPage: Parâmetro de navegação inválido ou ausente!");
            }
        }
    }
}