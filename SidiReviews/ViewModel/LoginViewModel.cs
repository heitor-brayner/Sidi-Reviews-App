using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SidiReviews.Interfaces;


namespace SidiReviews.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigatationService _navigationService;

        [ObservableProperty]
        private string _password;
        
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string? _errorMessage;

        
        public LoginViewModel(IAuthenticationService authenticationService, INavigatationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
        }
        [RelayCommand]
        private async Task LoginAsync()
        {
            ErrorMessage = null;

            var authenticatedUser = await _authenticationService.AuthenticateUserAsync(Username, Password);
            if (authenticatedUser != null)
            {
                _navigationService.NavigateTo("HomePage");
            }
            else
            {
                ErrorMessage = "Usuário ou senha inválidos";
                Password = string.Empty;
            }

        }

        public void ClearCredentials()
        {
            Username = string.Empty;
            Password = string.Empty;
            ErrorMessage = null;
        }

    }
}
