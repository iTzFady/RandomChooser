using System;
using System.Collections.Generic;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace RandomChooser.Controls
{
    /// <summary>
    /// Interaction logic for TextToggleButton.xaml
    /// </summary>
    public partial class TextToggleButton : UserControl
    {
        public TextToggleButton()
        {
            InitializeComponent();
        }

        private void DisplayModeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            Holder.FlowDirection = FlowDirection.RightToLeft;
            CurrentMode.Content = "Extend";
            Icon.Source = new BitmapImage(new Uri(@"/Assets/Images/2monitors.png", UriKind.Relative));
        }

        private void DisplayModeSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            Holder.FlowDirection = FlowDirection.LeftToRight;
            CurrentMode.Content = "Duplicate";
            Icon.Source = new BitmapImage(new Uri(@"/Assets/Images/monitor.png", UriKind.Relative));
        }
    }
}
