using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using RandomChooser.Services;

namespace RandomChooser.Views;

public partial class RandomWordControlPage : UserControl
{
    private readonly Random random = new();
    private readonly List<Item> items;
    private readonly Presenter presenter;

    public RandomWordControlPage()
    {
        items = WordGen.LoadWords().Words;
        InitializeComponent();
        presenter = new Presenter(this, ChosenWord, isNumbers: false);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        presenter.Start();
    }

    private void Pick_Click(object? sender, RoutedEventArgs e)
    {
        if (items.Count == 0)
        {
            return;
        }

        presenter.Update(items[random.Next(items.Count)].Name);
        SoundService.PlaySelection();
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
