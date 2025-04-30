using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RandomChooser.Controls
{
    /// <summary>
    /// Interaction logic for Textfield.xaml
    /// </summary>
    public partial class Textfield : UserControl
    {
        Regex regex = new Regex("[^0-9]+");
        public Textfield()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty TopTextProperty =
            DependencyProperty.Register("TopText", typeof(String), typeof(Textfield), new PropertyMetadata(string.Empty, ValueChangedTop));

        private static void ValueChangedTop(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Textfield;
            control.TopLabelText.Content = e.NewValue;
        }
        public string TopText
        {
            get { return (string)GetValue(TopTextProperty); }
            set
            {
                SetValue(TopTextProperty, value);
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (String)e.DataObject.GetData(typeof(string));
                if (regex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else { 
                e.CancelCommand();
            }
        }
    }
}
