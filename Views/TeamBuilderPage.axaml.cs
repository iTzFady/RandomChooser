using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using RandomChooser.Services;

namespace RandomChooser.Views;

public partial class TeamBuilderPage : UserControl
{
    public ObservableCollection<Team> Teams { get; } = new();

    public TeamBuilderPage()
    {
        DataContext = this;
        InitializeComponent();
        AutoCountToggle.PropertyChanged += AutoCountToggle_PropertyChanged;
        UpdateCountMode();
    }

    private void AutoCountToggle_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property.Name == nameof(ToggleSwitch.IsChecked))
        {
            UpdateCountMode();
        }
    }

    private void UpdateCountMode()
    {
        bool automatic = AutoCountToggle.IsChecked ?? false;
        TeamCountBox.IsEnabled = !automatic;
        TeamCountBox.Watermark = automatic ? "Auto" : "Count";
    }

    private async void Create_Click(object? sender, RoutedEventArgs e)
    {
        int? requestedCount = null;

        if (!(AutoCountToggle.IsChecked ?? false))
        {
            if (!FriendlyInput.TryReadInt(TeamCountBox, "team count", 1, int.MaxValue, out int count, out string error))
            {
                await FriendlyInput.ShowError(this, error);
                return;
            }

            requestedCount = count;
        }

        try
        {
            TeamBuildResult result = TeamBuilder.Build(NamesBox.Text, requestedCount);
            if (!result.Success)
            {
                await FriendlyInput.ShowError(this, result.Error!);
                return;
            }

            Teams.Clear();
            foreach (Team team in result.Teams)
            {
                Teams.Add(team);
            }

            SummaryText.Text = $"{result.NameCount} names → {result.TeamCount} teams";
            SoundService.PlaySelection();
        }
        catch (Exception)
        {
            await FriendlyInput.ShowUnexpected(this, "create the teams");
        }
    }

    private void Clear_Click(object? sender, RoutedEventArgs e)
    {
        NamesBox.Text = string.Empty;
        Teams.Clear();
        SummaryText.Text = "Enter names, then create teams.";
    }

    private void Home_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.GoBack();
    }
}
