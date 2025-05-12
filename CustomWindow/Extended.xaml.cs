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
        public Extended()
        {
            InitializeComponent();
            Loaded += Extended_Loaded;
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
        }

        public void UpdateNumber(string newNumber) {
            Number.Text = newNumber;
        }
        public void UpdateColor(Brush color) { 
            Number.Foreground = color;
            ExtendedLabelText.Foreground = color;
        }
    }
}
