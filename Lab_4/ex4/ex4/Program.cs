using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        
        string directoryPath = @"C:\denisko\Labs\OOP";

        
        Func<string, IEnumerable<string>> tokenize = TokenizeFile;
        Func<IEnumerable<string>, IDictionary<string, int>> countWords = CountWords;
        Action<IDictionary<string, int>> displayStats = DisplayStatistics;

        
        var files = Directory.EnumerateFiles(directoryPath);
        foreach (var file in files)
        {
            var words = tokenize(file);
            var wordCounts = countWords(words);
            displayStats(wordCounts);
        }

        Console.ReadLine();
    }

    
    static IEnumerable<string> TokenizeFile(string filePath)
    {
        string fileText = File.ReadAllText(filePath);
        var words = fileText.Split(' ', '\n', '\r')
                            .Select(w => w.Trim())
                            .Where(w => !string.IsNullOrEmpty(w));
        return words;
    }

    
    static IDictionary<string, int> CountWords(IEnumerable<string> words)
    {
        var wordCounts = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (wordCounts.ContainsKey(word))
            {
                wordCounts[word]++;
            }
            else
            {
                wordCounts[word] = 1;
            }
        }
        return wordCounts;
    }

    
    static void DisplayStatistics(IDictionary<string, int> wordCounts)
    {
        Console.WriteLine("Word frequencies:");
        foreach (var kvp in wordCounts.OrderByDescending(kvp => kvp.Value))
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        Console.WriteLine();
    }
}