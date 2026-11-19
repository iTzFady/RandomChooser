using Avalonia;
using Avalonia.Controls;
using RandomChooser.Views;

namespace RandomChooser.Services;

public static class FriendlyInput
{
    public static bool TryReadInt(
        TextBox? box,
        string fieldName,
        int min,
        int max,
        out int value,
        out string error)
    {
        value = 0;
        error = string.Empty;

        var text = box?.Text?.Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            error = $"Enter the {fieldName}.";
            return false;
        }

        if (!int.TryParse(text, out value))
        {
            error = $"The {fieldName} must be a whole number, like 10.";
            return false;
        }

        if (value < min || value > max)
        {
            error = max == int.MaxValue
                ? $"The {fieldName} must be {min} or more."
                : $"The {fieldName} must be between {min} and {max}.";
            return false;
        }

        return true;
    }

    public static Task ShowError(Visual host, string message)
    {
        return MessageWindow.Info(host, "Check your input", message);
    }

    public static Task ShowUnexpected(Visual host, string action)
    {
        return MessageWindow.Info(
            host,
            "Something went wrong",
            $"We couldn't {action}. Please try again.");
    }
}
