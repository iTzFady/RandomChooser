using RandomChooser.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var secondScreen = screens[1];
            var bounds = secondScreen.Bounds;

            Left = bounds.Left;
            Top = bounds.Top;
            Width = bounds.Width;
            Height = bounds.Height;
        }

        public void UpdateNumber(string newNumber) {
            Number.Text = newNumber;
        }
    }
}
