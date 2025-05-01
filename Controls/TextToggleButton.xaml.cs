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



        public bool State
        {
            get { return (bool)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(bool), typeof(TextToggleButton), new PropertyMetadata(true , ButtonValueChanged));

        private static void ButtonValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control =  d as TextToggleButton;
            control.DisplayModeSwitch.IsChecked = (bool)e.NewValue;
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
