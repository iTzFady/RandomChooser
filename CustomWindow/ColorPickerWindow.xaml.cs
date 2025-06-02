using System.Windows;
using WpfScreenHelper;
namespace RandomChooser.CustomWindow
{
    public partial class ColorPickerWindow : Window
    {
        public System.Windows.Media.Color SelectedColor { get; private set; }

        public ColorPickerWindow()
        {
            InitializeComponent();
            Loaded += ColorPickerWindow_Loaded;
        }

        private void ColorPickerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var screen = Screen.PrimaryScreen.WorkingArea;
            this.Left = screen.Left + (screen.Width - this.Width) / 2;
            this.Top = screen.Top + (screen.Height - this.Height) / 2;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = standard_picker.SelectedColor;
            DialogResult = true;
            Close();
        }
    }
}
