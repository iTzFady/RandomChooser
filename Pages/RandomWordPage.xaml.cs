using System;
using System.Collections.Generic;
using System.Configuration;
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

        private void StartWord_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationManager.RefreshSection("ApplicationSettings");
            if (WordGen.LoadWords().Words.Count > 1)
            {
                NavigationService.Navigate(new RandomWordControlPage());
            }
            else {
                MessageBox.Show("You must add Word List in the settings\n Make sure there is more than one word" , 
                    "Error" , 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
    }
}
