using RandomChooser.CustomWindow;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        bool display;
        public RandomNumberPage()
        {
            display = Screen.AllScreens.Count() > 1 ? SettingsSection.DisplayMode : false;
            _min = SettingsSection.MinRange;
            _max = SettingsSection.MaxRange;
            InitializeComponent();
            if (chosenNumber != null) {
                chosenNumber.Foreground = SettingsSection.TextColorBrush;
            }
            if (LabelText != null)
            {
                LabelText.Foreground = SettingsSection.TextColorBrush;
            }
            if (display)
            {
                extended.Background = SettingsSection.PageColorBrush;
                extended.Show();
                extended.UpdateColor(SettingsSection.TextColorBrush);
            }
            else {
                _mainWindow = Application.Current.MainWindow;
                this.Background = SettingsSection.PageColorBrush;
                _mainWindow.WindowStyle = WindowStyle.None;
                _mainWindow.WindowState = WindowState.Maximized;

            }
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            chosenNumber.Text = _gen.Next(_min, _max).ToString();
            if (display) {
                extended.UpdateNumber(chosenNumber.Text);
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (display)
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
