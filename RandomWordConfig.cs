using System.Collections.ObjectModel;
using System.Text.Json;
using RandomChooser.Services;

namespace RandomChooser;

public sealed class WordsList
{
    public List<Item> Words { get; set; } = new();
}

public static class WordGen
{
    public static WordsList LoadWords()
    {
        try
        {
            if (!File.Exists(AppSettings.WordsPath))
            {
                return new WordsList();
            }

            var json = File.ReadAllText(AppSettings.WordsPath);
            var list = JsonSerializer.Deserialize<WordsList>(json) ?? new WordsList();
            return Normalize(list);
        }
        catch
        {
            return new WordsList();
        }
    }

    public static void SaveWords(ObservableCollection<Item> items)
    {
        Save(new WordsList { Words = items.ToList() });
    }

    public static bool TrySaveWords(ObservableCollection<Item> items, out string error)
    {
        try
        {
            SaveWords(items);
            error = string.Empty;
            return true;
        }
        catch (Exception)
        {
            error = "We couldn't save your word list. Check that your data folder is writable, then try again.";
            return false;
        }
    }

    private static WordsList Normalize(WordsList list)
    {
        list.Words ??= new List<Item>();
        list.Words = list.Words
            .Where(item => item is not null && !string.IsNullOrWhiteSpace(item.Name))
            .Select(item => new Item { Name = item.Name.Trim() })
            .ToList();
        return list;
    }

    private static void Save(WordsList list)
    {
        var directory = Path.GetDirectoryName(AppSettings.WordsPath)!;
        Directory.CreateDirectory(directory);
        var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(AppSettings.WordsPath, json);
    }
}
