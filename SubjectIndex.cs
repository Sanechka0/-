using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SubjectIndex
{
    private readonly Dictionary<string, SortedSet<int>> _index = new();

    public void AddEntry(string word, IEnumerable<int> pages)
    {
        if (!_index.ContainsKey(word))
        {
            _index[word] = new SortedSet<int>();
        }
        foreach (var page in pages)
        {
            _index[word].Add(page);
        }
    }

    public bool RemoveEntry(string word) => _index.Remove(word);

    public SortedSet<int> GetPages(string word) => 
        _index.TryGetValue(word, out var pages) ? pages : new SortedSet<int>();

    public void Print()
    {
        foreach (var entry in _index.OrderBy(e => e.Key))
        {
            Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value)}");
        }
    }

    public void SaveToFile(string path)
    {
        using var writer = new StreamWriter(path);
        foreach (var entry in _index)
        {
            writer.WriteLine($"{entry.Key}:{string.Join(",", entry.Value)}");
        }
    }

    public static SubjectIndex LoadFromFile(string path)
    {
        var index = new SubjectIndex();
        foreach (var line in File.ReadAllLines(path))
        {
            var parts = line.Split(':', 2);
            if (parts.Length != 2) continue;
            
            var pages = parts[1].Split(',')
                .Select(p => int.TryParse(p.Trim(), out int page) ? page : 0)
                .Where(p => p > 0);
            
            index.AddEntry(parts[0].Trim(), pages);
        }
        return index;
    }

    public static SubjectIndex InputFromConsole()
    {
        var index = new SubjectIndex();
        Console.WriteLine("Введите данные (слово и страницы через пробел, пустая строка для завершения):");
        
        while (true)
        {
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) break;

            var parts = input.Split(' ');
            if (parts.Length < 2) continue;

            var pages = parts.Skip(1)
                .Select(p => int.TryParse(p, out int page) ? page : 0)
                .Where(p => p > 0);
            
            index.AddEntry(parts[0], pages);
        }
        return index;
    }
}