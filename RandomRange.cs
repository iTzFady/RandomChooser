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
    }
}
