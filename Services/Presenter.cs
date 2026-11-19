using Avalonia;
using Avalonia.Controls;
using RandomChooser.Views;

namespace RandomChooser.Services;

public sealed class Presenter
{
    private readonly UserControl host;
    private readonly TextBlock result;
    private readonly bool isNumbers;
    private Window? mainWindow;
    private ExtendedWindow? extended;
    private PixelPoint originalPosition;
    private double originalWidth;
    private double originalHeight;
    private WindowState originalState;
    private bool layoutCaptured;

    public Presenter(UserControl host, TextBlock result, bool isNumbers)
    {
        this.host = host;
        this.result = result;
        this.isNumbers = isNumbers;
    }

    public void Start()
    {
        mainWindow = TopLevel.GetTopLevel(host) as Window;
        if (mainWindow is null)
        {
            return;
        }

        mainWindow.Closing += MainWindow_Closing;
        CaptureLayout(mainWindow);
        SnapToPrimaryScreen(mainWindow);

        if (App.Settings.DisplayMode && mainWindow.Screens.All.Count > 1)
        {
            extended = new ExtendedWindow();
            extended.UpdateMode(isNumbers);
            extended.UpdateNumber(result.Text ?? string.Empty);
            extended.Show(mainWindow);
        }
        else
        {
            mainWindow.WindowState = WindowState.FullScreen;
        }
    }

    public void Update(string text)
    {
        result.Text = text;
        extended?.UpdateNumber(text);
    }

    public void Stop()
    {
        if (mainWindow is not null)
        {
            mainWindow.Closing -= MainWindow_Closing;

            if (layoutCaptured)
            {
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.Position = originalPosition;
                mainWindow.Width = originalWidth;
                mainWindow.Height = originalHeight;
                mainWindow.WindowState = originalState;
                layoutCaptured = false;
            }
        }

        extended?.Close();
        extended = null;
        mainWindow = null;
    }

    private void CaptureLayout(Window window)
    {
        originalPosition = window.Position;
        originalWidth = window.Width;
        originalHeight = window.Height;
        originalState = window.WindowState;
        layoutCaptured = true;
    }

    private static void SnapToPrimaryScreen(Window window)
    {
        var primary = window.Screens.Primary;
        if (primary is null)
        {
            return;
        }

        window.WindowState = WindowState.Normal;

        PixelRect area = primary.WorkingArea;
        double scale = primary.Scaling > 0 ? primary.Scaling : 1;

        window.Position = new PixelPoint(area.X, area.Y);
        window.Width = area.Width / scale;
        window.Height = area.Height / scale;
    }

    private void MainWindow_Closing(object? sender, WindowClosingEventArgs e)
    {
        extended?.Close();
        extended = null;
    }
}
