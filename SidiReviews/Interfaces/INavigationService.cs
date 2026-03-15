namespace SidiReviews.Interfaces
{

    public interface INavigatationService
    {
        void NavigateTo(string pageKey);
        void NavigateToReviews(string pageKey, object parameter);
        void GoBack();
    }
}
