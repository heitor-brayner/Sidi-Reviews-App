using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SidiReviews.Interfaces;

namespace SidiReviews.ViewModel
{
    public partial class NavbarViewModel : ObservableObject
    {
        private readonly INavigatationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public NavbarViewModel(INavigatationService navigatationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigatationService;
            NavigateCommand = new RelayCommand<string>(NaviagteTo);
            LogoutCommand = new RelayCommand<XamlRoot>(Logout);
            _authenticationService = authenticationService;
        }

        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }

        private void NaviagteTo(string pageKey)
        {
            _navigationService.NavigateTo(pageKey);
        }
        private async void Logout(XamlRoot xamlRoot)
        {
            var dialog = new ContentDialog
            {
                Title = "Confirmar saída",
                Content = "Você realmente deseja sair?",
                PrimaryButtonText = "Sim",
                CloseButtonText = "Não",
                XamlRoot = xamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await _authenticationService.LogoutUserAsync();
                _navigationService.NavigateTo("LoginPage");
            }
        }

    }
}
