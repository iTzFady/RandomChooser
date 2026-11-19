using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RandomChooser.Views;

public partial class MessageWindow : Window
{
    public MessageWindow()
    {
        InitializeComponent();
    }

    private MessageWindow(string title, string message, bool showCancel) : this()
    {
        Title = title;
        MessageText.Text = message;
        CancelButton.IsVisible = showCancel;
    }

    public static Task<bool> Confirm(Visual host, string title, string message)
    {
        return Show(host, title, message, showCancel: true);
    }

    public static Task<bool> Info(Visual host, string title, string message)
    {
        return Show(host, title, message, showCancel: false);
    }

    private static Task<bool> Show(Visual host, string title, string message, bool showCancel)
    {
        if (TopLevel.GetTopLevel(host) is not Window owner)
        {
            return Task.FromResult(false);
        }

        return new MessageWindow(title, message, showCancel).ShowDialog<bool>(owner);
    }

    private void Ok_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
