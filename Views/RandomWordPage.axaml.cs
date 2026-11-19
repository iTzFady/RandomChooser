using Avalonia.Controls;
using Avalonia.Interactivity;
using RandomChooser.Services;

namespace RandomChooser.Views;

public partial class RandomWordPage : UserControl
{
    public RandomWordPage()
    {
        InitializeComponent();
    }

    private void Start_Click(object? sender, RoutedEventArgs e)
    {
        if (WordGen.LoadWords().Words.Count > 1)
        {
            App.Navigation.Navigate(new RandomWordControlPage());
            return;
        }

        _ = MessageWindow.Info(this, "Error", "Add a word list with at least two words in settings first.");
    }

    private void Home_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.GoBack();
    }

    private void Settings_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new WordSettingsPage());
    }
}
