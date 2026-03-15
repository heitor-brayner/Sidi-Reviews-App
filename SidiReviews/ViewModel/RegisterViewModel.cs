using System.Net.Mail;
using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SidiReviews.Interfaces;

namespace SidiReviews.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigatationService _navigationService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string? _username;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string? _password;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string? _email;

        [ObservableProperty]
        private string? _errorMessage;
        
        public RegisterViewModel(IAuthenticationService authenticationService, INavigatationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
        }

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(Email);
        }

        [RelayCommand(CanExecute = nameof(CanRegister))]
        private async Task RegisterAsync()
        {
            ErrorMessage = null;
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Todos os campos são obrigatórios.";
                return;
            }
            try
            {
                var mailAddress = new MailAddress(Email);
            }
            catch (FormatException)
            {
                ErrorMessage = "Formato de e-mail inválido.";
                return;
            }
            if (Password.Length < 6)
            {
                ErrorMessage = "A senha deve ter pelo menos 6 caracteres.";
                return;
            }
            var errorMessage = await _authenticationService.RegisterUserAync(Username, Email, Password);
            if (errorMessage == null)
            {
                _navigationService.NavigateTo("LoginPage");
            }
            else
            {
                ErrorMessage = errorMessage;
            }
        }
        public void ClearCredentials()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;   
        }
    }
}
