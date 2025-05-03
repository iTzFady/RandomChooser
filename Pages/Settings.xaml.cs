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
using System.Windows.Shapes;
using System.Configuration;


namespace RandomChooser.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public Settings()
        {
            InitializeComponent();


            if (config.Sections["RandomRange"] is null)
            {
                config.Sections.Add("RandomRange", new RandomRange());
            }

            var SettingsSection = config.GetSection("RandomRange");

            this.DataContext = SettingsSection;
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            config.Save();
            System.Diagnostics.Debug.WriteLine(MinValue.TextFieldText);
            System.Diagnostics.Debug.WriteLine(MaxValue.TextFieldText);
            System.Diagnostics.Debug.WriteLine(MinValue.textBox.Text);
            System.Diagnostics.Debug.WriteLine(MaxValue.textBox.Text);
            //NavigationService.GoBack();
        }
    }
}
