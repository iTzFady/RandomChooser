using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using RandomChooser.Services;

namespace RandomChooser.Views;

public partial class WordSettingsPage : UserControl
{
    public ObservableCollection<Item> Items { get; }

    public WordSettingsPage()
    {
        Items = new ObservableCollection<Item>(WordGen.LoadWords().Words);
        DataContext = this;
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        DisplayToggle.IsEnabled = (TopLevel.GetTopLevel(this)?.Screens?.All.Count ?? 0) > 1;
    }

    private void Add_Click(object? sender, RoutedEventArgs e)
    {
        Items.Add(new Item());
    }

    private void Remove_Click(object? sender, RoutedEventArgs e)
    {
        if ((sender as Button)?.DataContext is Item item)
        {
            Items.Remove(item);
        }
    }

    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        var cleaned = Items
            .Where(item => !string.IsNullOrWhiteSpace(item.Name))
            .Select(item => new Item { Name = item.Name.Trim() })
            .ToList();

        Items.Clear();
        foreach (Item item in cleaned)
        {
            Items.Add(item);
        }

        string wordError = string.Empty;
        string settingsError = string.Empty;
        if (!WordGen.TrySaveWords(Items, out wordError) ||
            !App.Settings.TrySave(out settingsError))
        {
            await FriendlyInput.ShowError(this, string.IsNullOrEmpty(wordError) ? settingsError : wordError);
            return;
        }

        App.Navigation.GoBack();
    }

    private async void Reset_Click(object? sender, RoutedEventArgs e)
    {
        bool confirmed = await MessageWindow.Confirm(
            this, "Reset Confirmation", "Are you sure you want to clear the word list and reset the settings?");

        if (!confirmed)
        {
            return;
        }

        Items.Clear();
        string wordError = string.Empty;
        string settingsError = string.Empty;
        if (!WordGen.TrySaveWords(new ObservableCollection<Item>(), out wordError) ||
            !App.Settings.TrySave(out settingsError))
        {
            await FriendlyInput.ShowError(this, string.IsNullOrEmpty(wordError) ? settingsError : wordError);
            return;
        }

        App.Settings.Reset();
        if (!App.Settings.TrySave(out settingsError))
        {
            await FriendlyInput.ShowError(this, settingsError);
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
