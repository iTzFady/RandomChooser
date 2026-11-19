using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;

namespace RandomChooser.Services;

public sealed class NavigationService : INotifyPropertyChanged
{
    private readonly Stack<UserControl> _history = new();
    private UserControl? _current;

    public UserControl? Current
    {
        get => _current;
        private set
        {
            _current = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanGoBack)));
        }
    }

    public bool CanGoBack => _history.Count > 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void Navigate(UserControl page)
    {
        if (Current is not null)
        {
            _history.Push(Current);
        }

        Current = page;
    }

    public void GoBack()
    {
        if (_history.Count > 0)
        {
            Current = _history.Pop();
        }
    }
}
