using RandomChooser.CustomWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WordSettings_Page.xaml
    /// </summary>
    public partial class WordSettings_Page : Page
    {
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public ObservableCollection<Item> Items { get; set; }
        public WordSettings_Page()
        {
            InitializeComponent();
            if (config.Sections["RandomRange"] is null)
            {
                config.Sections.Add("RandomRange", new RandomRange());
            }
            if (config.Sections["ApplicationSettings"] is null)
            {
                config.Sections.Add("ApplicationSettings", new AppearanceSettings());
            }
            var WordList = WordGen.LoadWords();
            var AppSettings = config.GetSection("ApplicationSettings");
            Items = new ObservableCollection<Item>(WordList.Words);
            this.DataContext = new {
                AppearanceSetting = AppSettings,
                Words = Items
            };
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("ApplicationSettings");
            WordGen.SaveWords(Items);
            NavigationService.GoBack();
        }

        private void ___ColorIndicator__Click(object sender, RoutedEventArgs e)
        {
            var colorWindow = new ColorPickerWindow();
            if (colorWindow.ShowDialog() == true)
            {
                Color receivedColor = colorWindow.SelectedColor;
                SolidColorBrush solidColor = new SolidColorBrush(receivedColor);
                ___ColorIndicator_.Background = solidColor;
            }
        }

        private void PageColorExtended_Click(object sender, RoutedEventArgs e)
        {
            var colorWindow = new ColorPickerWindow();
            if (colorWindow.ShowDialog() == true)
            {
                Color pageReceivedColor = colorWindow.SelectedColor;
                SolidColorBrush solidColor = new SolidColorBrush(pageReceivedColor);
                PageColorExtended.Background = solidColor;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Items.Add(new Item());
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Item item)
                Items.Remove(item);
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = MessageBox.Show("Are you sure you want to reset the settings?", 
                "Reset Confirmation", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question,
                MessageBoxResult.No);
            if (messageBox == MessageBoxResult.Yes)
            {
                config.Sections.Clear();
                config.Save(ConfigurationSaveMode.Modified);
                WordGen.ClearWords();
                Items.Clear();
                ConfigurationManager.RefreshSection("ApplicationSettings");
                NavigationService.GoBack();
            }
        }
    }
}
