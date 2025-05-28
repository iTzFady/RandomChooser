using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RandomChooser
{
    class AppearanceSettings : ConfigurationSection
    {
        [ConfigurationProperty("DisplayMode", DefaultValue = true)]
        public bool DisplayMode
        {
            get { return (bool)this["DisplayMode"]; }
            set { this["DisplayMode"] = value; }
        }
        [ConfigurationProperty("Theme", DefaultValue = false)]
        public bool Theme
        {
            get { return (bool)this["Theme"]; }
            set { this["Theme"] = value; }
        }

        [ConfigurationProperty("TextColor", DefaultValue = "Black")]
        public Brush TextColorBrush
        {
            get { return (SolidColorBrush)this["TextColor"]; }
            set { this["TextColor"] = value; }
        }
        [ConfigurationProperty("PageColor", DefaultValue = "Gray")]
        public Brush PageColorBrush
        {
            get { return (SolidColorBrush)this["PageColor"]; }
            set { this["PageColor"] = value; }
        }
    }
}
