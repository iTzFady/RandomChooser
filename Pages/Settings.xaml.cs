using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Configuration;
using RandomChooser.CustomWindow;
using System.Windows.Media;
using WpfScreenHelper;


namespace RandomChooser.Pages
{
    public partial class Settings : Page
    {
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public Settings()
        {
            InitializeComponent();
            List<Screen> screens = Screen.AllScreens.ToList();
            PresentationMode.DisplayModeSwitch.IsEnabled = screens.Count > 1;

            if (config.Sections["RandomRange"] is null)
            {
                config.Sections.Add("RandomRange", new RandomRange());
            }

            var SettingsSection = config.GetSection("RandomRange");

            this.DataContext = SettingsSection;
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(MaxValue.textBox.Text) - int.Parse(MinValue.textBox.Text)) > 1)
            {
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("RandomRange");
                NavigationService.GoBack();
            }
            else {
                MessageBox.Show("Min value must be smaller than max value!",
                    "Configration Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

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

        private void PageColorIndicator_Click(object sender, RoutedEventArgs e)
        {
            var colorWindow = new ColorPickerWindow();
            if (colorWindow.ShowDialog() == true)
            {
                Color pageReceivedColor = colorWindow.SelectedColor;
                SolidColorBrush solidColor = new SolidColorBrush(pageReceivedColor);
                PageColorExtended.Background = solidColor;
            }
        }
    }
}
