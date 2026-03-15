using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using SidiReviews.Interfaces;
using SidiReviews.Views;
using SidiReviews;
using System.Collections.Generic;
using System;

public class NavigationService : INavigatationService
{
    private readonly Dictionary<string, Type> _page = new Dictionary<string, Type>
    {
        {"LoginPage", typeof(LoginPage) },
        {"RegisterPage", typeof(RegisterPage) },
        {"HomePage", typeof(HomePage) },
        {"MoviesPage", typeof(MoviesPage) },
        {"ReviewsPage", typeof(ReviewsPage) },
        {"ReviewFormPage", typeof(ReviewFormPage) },
        {"MyReviewsPage", typeof(MyReviewsPage) },
    };

    public void NavigateTo(string pageKey)
    {
        if (_page.TryGetValue(pageKey, out var pageType))
        {
            MainWindow.InstanceRootFrame.Navigate(pageType);
        }
        else
        {
            throw new ArgumentException($"No such page: {pageKey}", nameof(pageKey));
        }
    }

    public void NavigateToReviews(string pageKey, object parameter)
    {
        if (_page.TryGetValue(pageKey, out var pageType))
        {
            MainWindow.InstanceRootFrame.Navigate(pageType, parameter);
        }
        else
        {
            throw new ArgumentException($"No such page: {pageKey}", nameof(pageKey));
        }
    }

    public void GoBack()
    {
        var frame = (Frame)Window.Current.Content;
        if (frame.CanGoBack)
        {
            frame.GoBack();
        }
    }
}
