using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace RandomChooser.Views;

public partial class ExtendedWindow : Window
{
    public ExtendedWindow()
    {
        InitializeComponent();

        Screen? secondary = Screens.All.FirstOrDefault(screen => !screen.IsPrimary);
        if (secondary is not null)
        {
            PixelRect area = secondary.WorkingArea;
            double scale = secondary.Scaling > 0 ? secondary.Scaling : 1;

            Position = new PixelPoint(area.X, area.Y);
            Width = area.Width / scale;
            Height = area.Height / scale;
        }


    }

    public void UpdateNumber(string text)
    {
        Selected.Text = text;
    }

    public void UpdateMode(bool isNumbers)
    {
        ExtendedLabel.Text = isNumbers ? "The Winner is" : "Today's Pick";
        Selected.FontSize = isNumbers ? 300 : 150;
    }
}
