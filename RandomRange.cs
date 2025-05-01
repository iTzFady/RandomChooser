using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
