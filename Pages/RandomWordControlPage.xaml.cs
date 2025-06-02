using RandomChooser.CustomWindow;
using RandomChooser.Helpers;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using WpfScreenHelper;

namespace RandomChooser.Pages
{
    /// <summary>
    /// Interaction logic for RandomWordControlPage.xaml
    /// </summary>
    public partial class RandomWordControlPage : Page
    {
        AppearanceSettings? AppearanceSection = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("ApplicationSettings") as RandomChooser.AppearanceSettings;
        Extended extended;
        readonly Window _mainWindow;
        Window _extended_window;
        Random rand = new Random();
        bool display;
        List<Item> items;
        public RandomWordControlPage()
        {
            if (AppearanceSection is null)
            {
                AppearanceSection = new AppearanceSettings();
            }
            display = Screen.AllScreens.Count() > 1 ? AppearanceSection!.DisplayMode : false;
            var WordList = WordGen.LoadWords();
            items = WordList.Words;
            InitializeComponent();
            if (chosenWord != null)
            {
                chosenWord.Foreground = AppearanceSection.TextColorBrush;
            }
            if (LabelText != null)
            {
                LabelText.Foreground = AppearanceSection.TextColorBrush;
            }
            if (display)
            {
                extended = new Extended(false);
                extended.Background = AppearanceSection.PageColorBrush;
                extended.Show();
                extended.UpdateColor(AppearanceSection.TextColorBrush);
            }
            else
            {
                _mainWindow = Application.Current.MainWindow;
                this.Background = AppearanceSection.PageColorBrush;
                _mainWindow.WindowStyle = WindowStyle.None;
                _mainWindow.WindowState = WindowState.Maximized;

            }
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            chosenWord.Text = items[rand.Next(0 , items.Count)].Name;
            Utility.PlaySound("select.wav");
            if (display)
            {
                extended.UpdateNumber(chosenWord.Text);
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
            else
            {
                _mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                _mainWindow.WindowState = WindowState.Normal;
            }
            NavigationService.GoBack();
        }

    }
}
