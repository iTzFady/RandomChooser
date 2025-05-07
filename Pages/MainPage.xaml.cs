using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Runtime.InteropServices;
using WpfScreenHelper;
using System.Diagnostics;
using RandomChooser.CustomWindow;

namespace RandomChooser.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationManager.RefreshSection("RandomRange");
            NavigationService.Navigate(new RandomNumberPage());
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Settings());
        }
    }
}
