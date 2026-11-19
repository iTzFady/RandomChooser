using Avalonia.Controls;
using Avalonia.Interactivity;
using RandomChooser.Services;

namespace RandomChooser.Views;

public partial class SettingsPage : UserControl
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        MinBox.Text = App.Settings.MinRange.ToString(System.Globalization.CultureInfo.InvariantCulture);
        MaxBox.Text = App.Settings.MaxRange.ToString(System.Globalization.CultureInfo.InvariantCulture);
        DisplayToggle.IsEnabled = (TopLevel.GetTopLevel(this)?.Screens?.All.Count ?? 0) > 1;
    }

    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (!FriendlyInput.TryReadInt(MinBox, "minimum number", 1, int.MaxValue, out int min, out string error) ||
            !FriendlyInput.TryReadInt(MaxBox, "maximum number", 1, int.MaxValue, out int max, out error))
        {
            await FriendlyInput.ShowError(this, error);
            return;
        }

        if (max <= min)
        {
            await FriendlyInput.ShowError(this, "The maximum number must be greater than the minimum number.");
            return;
        }

        App.Settings.MinRange = min;
        App.Settings.MaxRange = max;
        if (!App.Settings.TrySave(out string saveError))
        {
            await FriendlyInput.ShowError(this, saveError);
            return;
        }

        App.Navigation.GoBack();
    }

    private async void Reset_Click(object? sender, RoutedEventArgs e)
    {
        bool confirmed = await MessageWindow.Confirm(
            this, "Reset Confirmation", "Are you sure you want to reset the settings?");

        if (!confirmed)
        {
            return;
        }

        App.Settings.Reset();
        if (!App.Settings.TrySave(out string saveError))
        {
            await FriendlyInput.ShowError(this, saveError);
            return;
        }

        App.Navigation.GoBack();
    }

    private async void PickTextColor_Click(object? sender, RoutedEventArgs e)
    {
        var color = await ColorPickerWindow.Pick(this, "Text color", App.Settings.GetTextColor());
        if (color.HasValue)
        {
            App.Settings.TextColor = ToHex(color.Value);
        }
    }

    private async void PickPageColor_Click(object? sender, RoutedEventArgs e)
    {
        var color = await ColorPickerWindow.Pick(this, "Background color", App.Settings.GetPageColor());
        if (color.HasValue)
        {
            App.Settings.PageColor = ToHex(color.Value);
        }
    }

    private static string ToHex(Avalonia.Media.Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}
