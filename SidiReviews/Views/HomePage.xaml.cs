using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using SidiReviews.ViewModel;
using Microsoft.UI;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SidiReviews.Views
{
    public sealed partial class HomePage : Page
    {
        private int _currentIndex = 0;
        private List<string> _imagePaths = new List<string>
        {
            "/Assets/anora.jpg",
            "/Assets/conclave.jpg",
            "/Assets/maateur.jpg",
            "/Assets/minecraft.jpg",
            "/Assets/warfare.jpg",
            "/Assets/sinners.jpg",
            "/Assets/prideandprejudice.jpg",
            "/Assets/mickey.jpg",
            "/Assets/companion.jpg",
        };

        private DispatcherTimer _autoScrollTimer;

        private List<SolidColorBrush> _indicatorColors = new List<SolidColorBrush>();

        public HomePage()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<HomeViewModel>();

            InitializeCarousel();

            StartAutoScroll();
        }

        private void InitializeCarousel()
        {
            UpdateIndicatorColors();

            CarouselIndicator.ItemsSource = _indicatorColors;

            UpdateImageItems();
        }

        private void UpdateIndicatorColors()
        {
            _indicatorColors.Clear();

            for (int i = 0; i <= _imagePaths.Count - 4; i++)
            {
                if (i == _currentIndex)
                {
                    _indicatorColors.Add(new SolidColorBrush(Colors.White));
                }
                else
                {
                    _indicatorColors.Add(new SolidColorBrush(Colors.Gray));
                }
            }

            if (CarouselIndicator != null)
            {
                CarouselIndicator.ItemsSource = null;
                CarouselIndicator.ItemsSource = _indicatorColors;
            }
        }

        private void StartAutoScroll()
        {
            _autoScrollTimer = new DispatcherTimer();
            _autoScrollTimer.Interval = TimeSpan.FromSeconds(5);
            _autoScrollTimer.Tick += AutoScrollTimer_Tick;
            _autoScrollTimer.Start();
        }

        private void AutoScrollTimer_Tick(object sender, object e)
        {
            if (_currentIndex < _imagePaths.Count - 4)
            {
                _currentIndex++;
            }
            else
            {
                _currentIndex = 0;
            }

            UpdateImageItems();
            UpdateIndicatorColors();
        }

        private void UpdateImageItems()
        {
            if (_currentIndex > _imagePaths.Count - 4)
            {
                _currentIndex = _imagePaths.Count - 4;
            }

            if (_currentIndex < 0)
            {
                _currentIndex = 0;
            }

            Image1.Source = new BitmapImage(new Uri(this.BaseUri, _imagePaths[_currentIndex]));
            Image2.Source = new BitmapImage(new Uri(this.BaseUri, _imagePaths[_currentIndex + 1]));
            Image3.Source = new BitmapImage(new Uri(this.BaseUri, _imagePaths[_currentIndex + 2]));
            Image4.Source = new BitmapImage(new Uri(this.BaseUri, _imagePaths[_currentIndex + 3]));
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAutoScrollTimer();

            if (_currentIndex > 0)
            {
                _currentIndex--;
                UpdateImageItems();
                UpdateIndicatorColors();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAutoScrollTimer();

            if (_currentIndex < _imagePaths.Count - 4)
            {
                _currentIndex++;
                UpdateImageItems();
                UpdateIndicatorColors();
            }
        }

        private void ResetAutoScrollTimer()
        {
            _autoScrollTimer.Stop();
            _autoScrollTimer.Start();
        }

        private void Image1_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                Frame.Navigate(typeof(MoviesPage));
            }
        }
    }
}