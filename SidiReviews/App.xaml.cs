using System;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SidiReviews.Commons;
using SidiReviews.Data;
using SidiReviews.Interfaces;
using SidiReviews.Services;
using SidiReviews.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SidiReviews
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public static IServiceProvider Services { get; set;  }
        public static Window m_window { get; set; }
        public App()
        {
            this.InitializeComponent();
        }

        public static IServiceProvider GetService()
        {
            return Services;
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<GlobalParameters>();
            services.AddDbContext<AppDbContext>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IReviewsService, ReviewsService>();
            services.AddSingleton<INavigatationService, NavigationService>();
            services.AddSingleton<ISessionService, SessionService>();

            //ViewModels
            services.AddSingleton<ReviewsViewModel>();
            services.AddSingleton<MoviesViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<NavbarViewModel>();
            services.AddSingleton<ReviewFormViewModel>();
            services.AddSingleton<MyReviewsViewModel>();
            return services.BuildServiceProvider();
        }

        private void InitializeDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var context = new AppDbContext(optionsBuilder.Options);
            context.Initialize();
        }


        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Ioc.Default.ConfigureServices(ConfigureServices());
            InitializeDatabase();
            m_window = new MainWindow();
            m_window.Activate();
        }

    }
}
