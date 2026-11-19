using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Media;

namespace RandomChooser.Services;

public sealed class AppSettings : INotifyPropertyChanged
{
    private static string _storageDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RandomChooser");

    internal static string StorageDirectory
    {
        get => _storageDirectory;
        set => _storageDirectory = value;
    }

    private static string SettingsPath => Path.Combine(StorageDirectory, "settings.json");

    public static string WordsPath => Path.Combine(StorageDirectory, "items.json");

    private int _minRange = 1;
    private int _maxRange = 100;
    private bool _displayMode = true;
    private bool _isDarkMode;
    private int _volume = 70;
    private string _textColor = "#191A1F";
    private string _pageColor = "#F5F6F8";

    public int MinRange
    {
        get => _minRange;
        set => Set(ref _minRange, value, nameof(MinRange));
    }

    public int MaxRange
    {
        get => _maxRange;
        set => Set(ref _maxRange, value, nameof(MaxRange));
    }

    public bool DisplayMode
    {
        get => _displayMode;
        set => Set(ref _displayMode, value, nameof(DisplayMode));
    }

    public bool IsDarkMode
    {
        get => _isDarkMode;
        set => Set(ref _isDarkMode, value, nameof(IsDarkMode));
    }

    public int Volume
    {
        get => _volume;
        set => Set(ref _volume, Math.Clamp(value, 0, 100), nameof(Volume));
    }

    public string TextColor
    {
        get => _textColor;
        set => Set(ref _textColor, value, nameof(TextColor), nameof(TextColorBrush));
    }

    public string PageColor
    {
        get => _pageColor;
        set => Set(ref _pageColor, value, nameof(PageColor), nameof(PageColorBrush));
    }

    [JsonIgnore]
    public IBrush TextColorBrush => new SolidColorBrush(ParseColor(TextColor));

    [JsonIgnore]
    public IBrush PageColorBrush => new SolidColorBrush(ParseColor(PageColor));

    public Color GetTextColor() => ParseColor(TextColor);

    public Color GetPageColor() => ParseColor(PageColor);

    public event PropertyChangedEventHandler? PropertyChanged;

    public static AppSettings Load()
    {
        if (!File.Exists(SettingsPath))
        {
            return new AppSettings();
        }

        try
        {
            var json = File.ReadAllText(SettingsPath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(StorageDirectory);
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsPath, json);
    }

    public bool TrySave(out string error)
    {
        try
        {
            Save();
            error = string.Empty;
            return true;
        }
        catch (Exception)
        {
            error = "We couldn't save your settings. Check that your data folder is writable, then try again.";
            return false;
        }
    }

    public void Reset()
    {
        MinRange = 1;
        MaxRange = 100;
        DisplayMode = true;
        IsDarkMode = false;
        Volume = 70;
        TextColor = "#191A1F";
        PageColor = "#F5F6F8";
    }

    private static Color ParseColor(string value)
    {
        try
        {
            return Color.Parse(value);
        }
        catch
        {
            return Color.Parse("#191A1F");
        }
    }

    private void Set<T>(ref T field, T value, string propertyName, params string[] extraProperties)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return;
        }

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        foreach (var name in extraProperties)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
