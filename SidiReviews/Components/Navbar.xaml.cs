using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using SidiReviews.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SidiReviews.Components
{
    public sealed partial class Navbar : UserControl
    {
        public Navbar()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<NavbarViewModel>();

        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is NavbarViewModel viewModel)
            {
                viewModel.LogoutCommand.Execute(this.XamlRoot);
            }
        }
    }
}
