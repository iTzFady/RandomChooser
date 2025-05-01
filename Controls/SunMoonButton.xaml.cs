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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace RandomChooser.Controls
{
    /// <summary>
    /// Interaction logic for SunMoonButton.xaml
    /// </summary>
    public partial class SunMoonButton : UserControl
    {
        public SunMoonButton()
        {
            InitializeComponent();
        }

        public bool Theme
        {
            get { return (bool)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("Theme", typeof(bool), typeof(SunMoonButton), new PropertyMetadata(true, ButtonValueChanged));

        private static void ButtonValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SunMoonButton;
            control.ThemeSwitch.IsChecked = (bool)e.NewValue;
        }



        private void ThemeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            InnerButton.HorizontalAlignment = HorizontalAlignment.Right;
            Uri resourseUI = new Uri("/Assets/Images/ButtonAnimationFrames/night-sky.jpg", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourseUI);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;
            brush.Stretch = Stretch.UniformToFill;
            ThemeSwitch.Background = brush;


            resourseUI = new Uri("/Assets/Images/moon.png", UriKind.Relative);
            streamInfo = Application.GetResourceStream(resourseUI);
            temp = BitmapFrame.Create(streamInfo.Stream);
            brush = new ImageBrush();
            brush.ImageSource = temp;
            brush.Stretch = Stretch.UniformToFill;
            InnerButton.Background = brush;
        }

        private void ThemeSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            InnerButton.HorizontalAlignment = HorizontalAlignment.Left;
            Uri resourseUI = new Uri("/Assets/Images/ButtonAnimationFrames/Sunny.jpg", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourseUI);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;
            brush.Stretch = Stretch.UniformToFill;
            ThemeSwitch.Background = brush;


            resourseUI = new Uri("/Assets/Images/sun.png", UriKind.Relative);
            streamInfo = Application.GetResourceStream(resourseUI);
            temp = BitmapFrame.Create(streamInfo.Stream);
            brush = new ImageBrush();
            brush.ImageSource = temp;
            brush.Stretch = Stretch.UniformToFill;
            InnerButton.Background = brush;
        }
    }
}
