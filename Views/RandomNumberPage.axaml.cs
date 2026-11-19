using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using RandomChooser.Services;

namespace RandomChooser.Views;

public partial class RandomNumberPage : UserControl
{
    private readonly NumberPicker picker = new();
    private readonly Presenter presenter;

    public RandomNumberPage()
    {
        InitializeComponent();
        presenter = new Presenter(this, ChosenNumber, isNumbers: true);
    }

    private void Start_Click(object? sender, RoutedEventArgs e)
    {
        presenter.Start();
        StartPanel.IsVisible = false;
        PickPanel.IsVisible = true;
    }

    private void Pick_Click(object? sender, RoutedEventArgs e)
    {
        presenter.Update(picker.Next(App.Settings.MinRange, App.Settings.MaxRange).ToString());
        SoundService.PlaySelection();
    }

    private void Settings_Click(object? sender, RoutedEventArgs e)
    {
        presenter.Stop();
        StartPanel.IsVisible = true;
        PickPanel.IsVisible = false;
        App.Navigation.Navigate(new SettingsPage());
    }

    private void Home_Click(object? sender, RoutedEventArgs e)
    {
        presenter.Stop();
        App.Navigation.GoBack();
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        presenter.Stop();
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
}
