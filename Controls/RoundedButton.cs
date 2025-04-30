using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Colour = System.Drawing.Color;
namespace RandomChooser.Controls
{
    public class RoundedButton : ButtonBase
    {
        public CornerRadius Corner
        {
            get { return (CornerRadius)GetValue(CornerProperty); }
            set { SetValue(CornerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Corner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerProperty =
            DependencyProperty.Register("Corner", typeof(CornerRadius), typeof(RoundedButton), new PropertyMetadata(new CornerRadius(0)));




        public Brush OnHoverTextColor
        {
            get { return (Brush)GetValue(onHoverTextColorProperty); }
            set { SetValue(onHoverTextColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for onHoverTextColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty onHoverTextColorProperty =
            DependencyProperty.Register("OnHoverTextColor", typeof(Brush), typeof(RoundedButton), new PropertyMetadata(new SolidColorBrush(Colors.White)));




        public Brush OnHoverBackground
        {
            get { return (Brush)GetValue(onHoverBackgroundProperty); }
            set { SetValue(onHoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for onHoverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty onHoverBackgroundProperty =
            DependencyProperty.Register("OnHoverBackground", typeof(Brush), typeof(RoundedButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));




        static RoundedButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundedButton), new FrameworkPropertyMetadata(typeof(RoundedButton)));
        }
    }
}
