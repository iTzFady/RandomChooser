using System.Windows;
using System.Windows.Media;
using WpfScreenHelper;

namespace RandomChooser.CustomWindow
{
    /// <summary>
    /// Interaction logic for Extended.xaml
    /// </summary>
    public partial class Extended : Window
    {
        bool isNumbers = true;
        public Extended(bool _currentMode)
        {
            InitializeComponent();
            Loaded += Extended_Loaded;
            isNumbers = _currentMode;
        }

        private void Extended_Loaded(object sender, RoutedEventArgs e)
        {
            List<Screen> screens = Screen.AllScreens.ToList();
            if (screens.Count() > 1) {
                var secondScreen = screens[1];
                var bounds = secondScreen.Bounds;

                Left = bounds.Left;
                Top = bounds.Top;
                Width = bounds.Width;
                Height = bounds.Height;
            }
            ExtendedLabelText.Content = isNumbers ? "The Winner is" : "Today’s Pick";
            Selected.FontSize = isNumbers ? 300 : 150;
        }

        public void UpdateNumber(string newNumber) {
            Selected.Text = newNumber;
        }
        public void UpdateColor(Brush color) { 
            Selected.Foreground = color;
            ExtendedLabelText.Foreground = color;
        }
    }
}
