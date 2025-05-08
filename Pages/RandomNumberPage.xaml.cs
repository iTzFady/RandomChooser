using RandomChooser.CustomWindow;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WpfScreenHelper;


namespace RandomChooser.Pages
{
    /// <summary>
    /// Interaction logic for RandomNumberPage.xaml
    /// </summary>
    public partial class RandomNumberPage : Page
    {
        int _min = 0;
        int _max = 0;
        RandomRange? SettingsSection = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("RandomRange") as RandomChooser.RandomRange;
        RandomGen _gen = new RandomGen();
        Extended extended = new Extended();
        readonly Window _mainWindow;
        Window _extended_window;
        public RandomNumberPage()
        {
            _min = SettingsSection.MinRange;
            _max = SettingsSection.MaxRange;
            InitializeComponent();
            if (SettingsSection.DisplayMode)
            {
                extended.Show();
            }
            else {
                _mainWindow = Application.Current.MainWindow;

                _mainWindow.WindowStyle = WindowStyle.None;
                _mainWindow.WindowState = WindowState.Maximized;

            }
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            chosenNumber.Text = _gen.Next(_min, _max).ToString();
            Debug.WriteLine(chosenNumber.Text);
            if (SettingsSection.DisplayMode) {
                extended.UpdateNumber(chosenNumber.Text);
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SettingsSection.DisplayMode)
            {
                _extended_window = Window.GetWindow(extended);
                _extended_window.Close();
            }
            else {
                _mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                _mainWindow.WindowState = WindowState.Normal;
            }
            NavigationService.GoBack();
        }
    }
}
