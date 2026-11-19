using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using RandomChooser.Services;
using RandomChooser.Views;

namespace RandomChooser;

public partial class App : Application
{
    public static AppSettings Settings { get; } = AppSettings.Load();

    public static NavigationService Navigation { get; } = new();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Settings.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(AppSettings.IsDarkMode) || string.IsNullOrEmpty(e.PropertyName))
            {
                ApplyTheme();
            }

            if (e.PropertyName is nameof(AppSettings.IsDarkMode) or nameof(AppSettings.Volume))
            {
                string ignored = string.Empty;
                Settings.TrySave(out ignored);
            }
        };
        ApplyTheme();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            Navigation.Navigate(new MainPage());
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ApplyTheme()
    {
        RequestedThemeVariant = Settings.IsDarkMode ? ThemeVariant.Dark : ThemeVariant.Light;
    }
}
