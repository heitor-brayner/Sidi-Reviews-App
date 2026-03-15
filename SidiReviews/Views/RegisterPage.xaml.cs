using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SidiReviews.ViewModel;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SidiReviews.Views
{
    public sealed partial class RegisterPage : Page
    {
        public RegisterViewModel ViewModel { get; set; }
        public RegisterPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<RegisterViewModel>();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.ClearCredentials();
        }

        private void HyperlinkButton_Click_Login(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
