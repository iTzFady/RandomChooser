using Avalonia.Media;
using RandomChooser.Services;
using Xunit;

namespace RandomChooser.Tests;

public class AppSettingsTests : StorageTestBase
{
    [Fact]
    public void Constructor_UsesExpectedDefaults()
    {
        var settings = new AppSettings();

        Assert.Equal(1, settings.MinRange);
        Assert.Equal(100, settings.MaxRange);
        Assert.True(settings.DisplayMode);
        Assert.False(settings.IsDarkMode);
        Assert.Equal(70, settings.Volume);
        Assert.Equal("#191A1F", settings.TextColor);
        Assert.Equal("#F5F6F8", settings.PageColor);
    }

    [Fact]
    public void Volume_ClampsToAllowedRange()
    {
        var settings = new AppSettings();

        settings.Volume = 150;
        Assert.Equal(100, settings.Volume);

        settings.Volume = -10;
        Assert.Equal(0, settings.Volume);
    }

    [Fact]
    public void Set_RaisesPropertyEmptyAndExtraChangeNotifications()
    {
        var settings = new AppSettings();
        var changed = new List<string?>();
        settings.PropertyChanged += (_, args) => changed.Add(args.PropertyName);

        settings.TextColor = "#FFFFFF";

        Assert.Equal(new[] { nameof(AppSettings.TextColor), string.Empty, nameof(AppSettings.TextColorBrush) }, changed);
    }

    [Fact]
    public void SaveAndLoad_RoundTripsValues()
    {
        var settings = new AppSettings
        {
            MinRange = 5,
            MaxRange = 25,
            DisplayMode = false,
            IsDarkMode = true,
            Volume = 35,
            TextColor = "#112233",
            PageColor = "#445566"
        };

        settings.Save();
        AppSettings loaded = AppSettings.Load();

        Assert.Equal(5, loaded.MinRange);
        Assert.Equal(25, loaded.MaxRange);
        Assert.False(loaded.DisplayMode);
        Assert.True(loaded.IsDarkMode);
        Assert.Equal(35, loaded.Volume);
        Assert.Equal("#112233", loaded.TextColor);
        Assert.Equal("#445566", loaded.PageColor);
    }

    [Fact]
    public void Reset_RestoresDefaults()
    {
        var settings = new AppSettings
        {
            MinRange = 10,
            MaxRange = 20,
            IsDarkMode = true,
            Volume = 5,
            TextColor = "#000000",
            PageColor = "#FFFFFF",
            DisplayMode = false
        };

        settings.Reset();

        Assert.Equal(1, settings.MinRange);
        Assert.Equal(100, settings.MaxRange);
        Assert.True(settings.DisplayMode);
        Assert.False(settings.IsDarkMode);
        Assert.Equal(70, settings.Volume);
        Assert.Equal("#191A1F", settings.TextColor);
        Assert.Equal("#F5F6F8", settings.PageColor);
    }

    [Fact]
    public void TrySave_WithInvalidStoragePath_ReturnsFriendlyError()
    {
        string invalidPath = Path.Combine(StoragePath, "not-a-directory.txt");
        File.WriteAllText(invalidPath, "content");
        AppSettings.StorageDirectory = invalidPath;

        var settings = new AppSettings();
        bool saved = settings.TrySave(out string error);

        Assert.False(saved);
        Assert.Contains("couldn't save your settings", error, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GetTextColor_WithInvalidHex_FallsBackToDefault()
    {
        var settings = new AppSettings { TextColor = "not-a-color" };

        Assert.Equal(Color.Parse("#191A1F"), settings.GetTextColor());
    }
}
