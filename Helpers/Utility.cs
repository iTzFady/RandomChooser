using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RandomChooser.Helpers
{
    public static class Utility
    {
        public static void PlaySound(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string fullName = $"RandomChooser.Assets.Audio.{resourceName}";

            using (var stream = assembly.GetManifestResourceStream(fullName))
            {
                if (stream != null)
                {
                    SoundPlayer player = new SoundPlayer(stream);
                    player.Play();
                }
                else
                {
                    throw new Exception($"Resource '{fullName}' not found.");
                }
            }
        }
    }
}
