using CommunityToolkit.Mvvm.ComponentModel;
using SidiReviews.Interfaces;

namespace SidiReviews.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ISessionService _sessionService;

        [ObservableProperty]
        private string _username;

        public HomeViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
            LoadUserData();
        }
        private void LoadUserData() 
        {
            var loggedUser = _sessionService.CurrentUser;
            if (loggedUser != null) 
            {
                Username = loggedUser.UserName;
            }
        }
    }
}
