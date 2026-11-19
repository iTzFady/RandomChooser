using System.Diagnostics;
using RandomChooser.Services;
using Xunit;

namespace RandomChooser.Tests;

public class SoundServiceTests : StorageTestBase
{
    [Fact]
    public void PlaySelection_DoesNotBlockCallerWhilePlayerRuns()
    {
        if (!OperatingSystem.IsLinux())
        {
            return;
        }

        string playerDirectory = Path.Combine(StoragePath, "players");
        Directory.CreateDirectory(playerDirectory);
        string fakePlayer = Path.Combine(playerDirectory, "paplay");
        File.WriteAllText(fakePlayer, "#!/bin/sh\nsleep 2\n");
        Chmod(fakePlayer, "755");

        string originalPath = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
        int originalVolume = App.Settings.Volume;
        var tempBefore = Directory.GetFiles(Path.GetTempPath(), "random-chooser-*.wav").ToHashSet();

        try
        {
            Environment.SetEnvironmentVariable("PATH",
                playerDirectory + Path.PathSeparator + originalPath);
            App.Settings.Volume = 70;

            var stopwatch = Stopwatch.StartNew();
            SoundService.PlaySelection();
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds < 1000,
                $"PlaySelection blocked the UI for {stopwatch.ElapsedMilliseconds} ms.");
        }
        finally
        {
            App.Settings.Volume = originalVolume;
            Environment.SetEnvironmentVariable("PATH", originalPath);
        }

        Thread.Sleep(2500);

        var tempAfter = Directory.GetFiles(Path.GetTempPath(), "random-chooser-*.wav").ToHashSet();
        Assert.Equal(tempBefore, tempAfter);
    }

    private static void Chmod(string path, string mode)
    {
        using var process = Process.Start("chmod", new[] { mode, path })!;
        process.WaitForExit();
        Assert.Equal(0, process.ExitCode);
    }
}
