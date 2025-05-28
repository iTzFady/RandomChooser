using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace RandomChooser
{
    public class WordsList
    {
        public List<Item> Words { get; set; } = new();
    }
    public static class WordGen {
        private const string ConfigFile = "items.json";


        public static WordsList LoadWords() {
            if (!File.Exists(ConfigFile))
                return new WordsList();

            var json = File.ReadAllText(ConfigFile);
            return JsonSerializer.Deserialize<WordsList>(json) ?? new WordsList();
        }
        public static void SaveWords(ObservableCollection<Item> items)
        {
            var config = new WordsList { Words = items.ToList() };
            var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFile, json);
        }
    }
}
