using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using WpfScreenHelper;
namespace RandomChooser.CustomWindow
{
    /// <summary>
    /// Interaction logic for ColorPickerWindow.xaml
    /// </summary>
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
