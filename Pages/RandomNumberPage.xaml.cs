using RandomChooser.CustomWindow;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfScreenHelper;
using RandomChooser.Helpers;

namespace RandomChooser.Pages
{
    /// <summary>
    /// Interaction logic for RandomNumberPage.xaml
    /// </summary>
    public partial class RandomNumberPage : Page
    {
        int _min;
        int _max;
        RandomRange? SettingsSection = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("RandomRange") as RandomChooser.RandomRange;
        AppearanceSettings? AppearanceSection = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("ApplicationSettings") as RandomChooser.AppearanceSettings;
        RandomGen _gen = new RandomGen();
        Extended extended;
        readonly Window _mainWindow;
        Window _extended_window;
        bool display;
        public RandomNumberPage()
        {
            if (SettingsSection is null)
            {
                SettingsSection = new RandomRange();
            }
            if (AppearanceSection is null) 
            { 
                AppearanceSection = new AppearanceSettings();
            }
            display = Screen.AllScreens.Count() > 1 ? AppearanceSection.DisplayMode : false;
            _min = SettingsSection.MinRange;
            _max = SettingsSection.MaxRange;
            InitializeComponent();
            if (chosenNumber != null) {
                chosenNumber.Foreground = AppearanceSection.TextColorBrush;
            }
            if (LabelText != null)
            {
                LabelText.Foreground = AppearanceSection.TextColorBrush;
            }
            if (display)
            {
                extended = new Extended(true);
                extended.Background = AppearanceSection.PageColorBrush;
                extended.Show();
                extended.UpdateColor(AppearanceSection.TextColorBrush);
            }
            else {
                _mainWindow = Application.Current.MainWindow;
                this.Background = AppearanceSection.PageColorBrush;
                _mainWindow.WindowStyle = WindowStyle.None;
                _mainWindow.WindowState = WindowState.Maximized;

            }
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            chosenNumber.Text = _gen.Next(_min, _max).ToString();
            Utility.PlaySound("select.wav");
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
