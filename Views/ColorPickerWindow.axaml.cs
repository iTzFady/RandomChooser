using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace RandomChooser.Views;

public partial class ColorPickerWindow : Window
{
    private Color current = Colors.White;
    private bool updating;

    public ColorPickerWindow() : this(Color.FromRgb(79, 70, 229))
    {
    }

    public ColorPickerWindow(Color initial)
    {
        InitializeComponent();

        RedSlider.PropertyChanged += Slider_PropertyChanged;
        GreenSlider.PropertyChanged += Slider_PropertyChanged;
        BlueSlider.PropertyChanged += Slider_PropertyChanged;
        HexBox.PropertyChanged += HexBox_PropertyChanged;

        current = initial;
        UpdateUi(writeHex: true);
    }

    public static async Task<Color?> Pick(Visual host, string title, Color? initial = null)
    {
        if (TopLevel.GetTopLevel(host) is not Window owner)
        {
            return null;
        }

        var window = new ColorPickerWindow(initial ?? Color.FromRgb(79, 70, 229)) { Title = title };
        return await window.ShowDialog<Color?>(owner);
    }

    private void Slider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property.Name != nameof(Slider.Value) || updating)
        {
            return;
        }

        current = Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value);
        UpdateUi(writeHex: true);
    }

    private void HexBox_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property.Name != nameof(TextBox.Text) || updating)
        {
            return;
        }

        try
        {
            var parsed = Color.Parse(HexBox.Text ?? string.Empty);
            current = Color.FromRgb(parsed.R, parsed.G, parsed.B);
            HexHint.IsVisible = false;
            UpdateUi(writeHex: false);
        }
        catch (FormatException)
        {
            HexHint.IsVisible = !string.IsNullOrWhiteSpace(HexBox.Text);
        }
        catch (InvalidOperationException)
        {
            HexHint.IsVisible = !string.IsNullOrWhiteSpace(HexBox.Text);
        }
    }

    private void UpdateUi(bool writeHex)
    {
        updating = true;
        Preview.Background = new SolidColorBrush(current);
        RedSlider.Value = current.R;
        GreenSlider.Value = current.G;
        BlueSlider.Value = current.B;
        if (writeHex)
        {
            HexBox.Text = ToHex(current);
        }
        updating = false;
    }

    private static string ToHex(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        Close(current);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(null);
    }
}
