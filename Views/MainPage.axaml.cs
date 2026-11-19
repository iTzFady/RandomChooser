using Avalonia.Controls;
using Avalonia.Interactivity;
using RandomChooser.Services;

namespace RandomChooser.Views;

public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void Number_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new RandomNumberPage());
    }

    private void Word_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new RandomWordPage());
    }

    private void Team_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new TeamBuilderPage());
    }

    private void Settings_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new SettingsPage());
    }
}
