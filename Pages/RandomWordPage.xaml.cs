using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomChooser.Pages
{
    /// <summary>
    /// Interaction logic for RandomWordPage.xaml
    /// </summary>
    public partial class RandomWordPage : Page
    {
        public RandomWordPage()
        {
            InitializeComponent();
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WordSettings_Page());
        }
    }
}
