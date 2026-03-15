using System;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SidiReviews.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SidiReviews.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginPage()
        {
            this.InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<LoginViewModel>();
            DataContext = ViewModel;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.ClearCredentials();
        }

        private void HyperlinkButton_Click_Register(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
