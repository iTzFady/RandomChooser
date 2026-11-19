using System.Diagnostics;
using System.Reflection;

namespace RandomChooser.Services;

public static class SoundService
{
    private const string ResourceName = "RandomChooser.Assets.Audio.select.wav";

    public static void PlaySelection()
    {
        try
        {
            int volume = Math.Clamp(App.Settings.Volume, 0, 100);
            if (volume == 0)
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(ResourceName);
            if (stream is null)
            {
                return;
            }

            using var memory = new MemoryStream();
            stream.CopyTo(memory);
            byte[] wav = memory.ToArray();

            if (volume < 100)
            {
                ApplyVolume(wav, volume / 100.0);
            }

            var temp = Path.Combine(Path.GetTempPath(), $"random-chooser-{Guid.NewGuid():N}.wav");
            try
            {
                File.WriteAllBytes(temp, wav);
                PlayWithExternalPlayer(temp);
            }
            finally
            {
                try
                {
                    File.Delete(temp);
                }
                catch
                {
                }
            }
        }
        catch
        {
            // Audio is cosmetic; never interrupt a pick.
        }
    }

    private static void ApplyVolume(byte[] wav, double factor)
    {
        int offset = FindDataOffset(wav);
        for (int i = offset; i + 1 < wav.Length; i += 2)
        {
            int sample = BitConverter.ToInt16(wav, i);
            int scaled = (int)Math.Clamp(sample * factor, short.MinValue, short.MaxValue);
            wav[i] = (byte)(scaled & 0xFF);
            wav[i + 1] = (byte)((scaled >> 8) & 0xFF);
        }
    }

    private static int FindDataOffset(byte[] wav)
    {
        for (int i = 12; i <= wav.Length - 8; i++)
        {
            if (wav[i] == (byte)'d' && wav[i + 1] == (byte)'a' &&
                wav[i + 2] == (byte)'t' && wav[i + 3] == (byte)'a')
            {
                return i + 8;
            }
        }

        throw new InvalidDataException("WAV data chunk not found.");
    }

    private static void PlayWithExternalPlayer(string path)
    {
        if (OperatingSystem.IsWindows())
        {
            TryStart("powershell", startInfo =>
            {
                startInfo.ArgumentList.Add("-NoProfile");
                startInfo.ArgumentList.Add("-Command");
                startInfo.ArgumentList.Add($"(New-Object System.Media.SoundPlayer '{path.Replace("'", "''")}').PlaySync()");
            });
            return;
        }

        string[] players = OperatingSystem.IsMacOS()
            ? new[] { "afplay" }
            : new[] { "paplay", "aplay", "ffplay" };

        foreach (var player in players)
        {
            if (TryStart(player, startInfo => startInfo.ArgumentList.Add(path)))
            {
                return;
            }
        }
    }

    private static bool TryStart(string file, Action<ProcessStartInfo> configure)
    {
        try
        {
            var startInfo = new ProcessStartInfo(file)
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };

            configure(startInfo);

            using var process = Process.Start(startInfo);
            if (process is null)
            {
                return false;
            }

            process.WaitForExit();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
