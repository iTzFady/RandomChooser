using System.ComponentModel;

namespace RandomChooser;

public sealed class Item : INotifyPropertyChanged
{
    private string name = string.Empty;

    public string Name
    {
        get => name;
        set
        {
            if (name == value)
            {
                return;
            }

            name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
