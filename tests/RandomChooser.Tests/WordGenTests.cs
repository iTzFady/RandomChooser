using System.Collections.ObjectModel;
using RandomChooser;
using RandomChooser.Services;
using Xunit;

namespace RandomChooser.Tests;

public class WordGenTests : StorageTestBase
{
    [Fact]
    public void LoadWords_WithNoFile_ReturnsEmptyList()
    {
        WordsList words = WordGen.LoadWords();

        Assert.Empty(words.Words);
    }

    [Fact]
    public void LoadWords_RemovesInvalidEntriesAndTrimsNames()
    {
        File.WriteAllText(AppSettings.WordsPath,
            "{\"Words\":[null,{\"Name\":\"  Alex  \"},{\"Name\":\"   \"},{\"Name\":\"Bob\"}]}");

        WordsList words = WordGen.LoadWords();

        Assert.Equal(new[] { "Alex", "Bob" }, words.Words.Select(word => word.Name));
    }

    [Fact]
    public void SaveWordsAndLoad_RoundTripsNames()
    {
        var items = new ObservableCollection<Item>
        {
            new() { Name = "Alice" },
            new() { Name = "Bob" }
        };

        WordGen.SaveWords(items);
        WordsList loaded = WordGen.LoadWords();

        Assert.Equal(new[] { "Alice", "Bob" }, loaded.Words.Select(word => word.Name));
    }

    [Fact]
    public void TrySaveWords_WithInvalidStoragePath_ReturnsFriendlyError()
    {
        string invalidPath = Path.Combine(StoragePath, "not-a-directory.txt");
        File.WriteAllText(invalidPath, "content");
        AppSettings.StorageDirectory = invalidPath;

        bool saved = WordGen.TrySaveWords(new ObservableCollection<Item>(), out string error);

        Assert.False(saved);
        Assert.Contains("couldn't save your word list", error, StringComparison.OrdinalIgnoreCase);
    }
}
