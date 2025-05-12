using System.Configuration;
using System.Windows.Media;

namespace RandomChooser
{
    internal class RandomRange : ConfigurationSection
    {
		[ConfigurationProperty("MinRange", DefaultValue = 1)]
		public int MinRange
		{
			get { return (int)this["MinRange"]; }
			set { this["MinRange"] = value; }
		}
        [ConfigurationProperty("MaxRange", DefaultValue = 100)]
        public int MaxRange
        {
            get { return (int)this["MaxRange"]; }
            set { this["MaxRange"] = value; }
        }
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
