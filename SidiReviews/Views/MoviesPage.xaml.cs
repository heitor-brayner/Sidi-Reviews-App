using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SidiReviews.ViewModel;
using System;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SidiReviews.Views
{
    public sealed partial class MoviesPage : Page
    {
        public MoviesViewModel ViewModel { get; }

        public MoviesPage()
        {
            this.InitializeComponent();

            var viewModel = Ioc.Default.GetRequiredService<MoviesViewModel>();
            if (viewModel == null)
            {
                throw new InvalidOperationException("Could not resolve MoviesViewModel from service provider.");
            }
            ViewModel = viewModel;
            this.DataContext = ViewModel;

            
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ViewModel?.LoadMoviesCommand != null && ViewModel.LoadMoviesCommand.CanExecute(null))
            {
                System.Diagnostics.Debug.WriteLine("OnNavigatedTo: Chamando LoadMoviesAsyncCommand...");
                await ViewModel.LoadMoviesCommand.ExecuteAsync(null);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnNavigatedTo: Não foi possível executar LoadMoviesAsyncCommand.");
            }
        }
    }
}