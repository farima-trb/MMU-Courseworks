// Program.cs
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ADS_final;

namespace ADS_final
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt for the input file path at startup
            Console.Write("Enter full path to text file: ");
            string filePath = Console.ReadLine()                  // read raw input
                             .Trim()                      
                             .Trim('"', '\'');            // remove surrounding " or '

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found: {filePath}");
                return;
            }

            // Analyse immediately
            var analyzer = new TextAnalyzer();
            analyzer.AnalyseFile(filePath);
            Console.WriteLine("File loaded and analysed successfully.\nPress Enter to continue...");
            Console.ReadLine();

            // Main menu loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Text Analysis Tool ===");
                Console.WriteLine("1) Display number of unique words");
                Console.WriteLine("2) Display all words and counts (in insertion order)");
                Console.WriteLine("3) Display all words and counts (alphabetical order)");
                Console.WriteLine("4) Output the longest word and its count");
                Console.WriteLine("5) Output the most frequent word and its count");
                Console.WriteLine("6) Given a word, display the line numbers where it appears");
                Console.WriteLine("7) Given a word, display its total frequency");
                Console.WriteLine("8) Exit");
                Console.Write("\nChoose an option (1–8): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "8")
                    break;

                switch (choice)
                {
                    case "1":
                        // Feature 2: number of distinct words
                        int uniqueCount = analyzer.GetUniqueWordCount();
                        Console.WriteLine($"Total unique words: {uniqueCount}");
                        break;

                    case "2":
                        // Feature 4: any order (insertion/pre-order)
                        Console.WriteLine("Word\tCount");
                        Console.WriteLine(new string('-', 30));
                        foreach (var wi in analyzer.GetAllWordsUnsorted())
                            Console.WriteLine($"{wi.Word}\t{wi.Count}");
                        break;

                    case "3":
                        // Feature 5: alphabetical order
                        Console.WriteLine("Word\tCount");
                        Console.WriteLine(new string('-', 30));
                        foreach (var wi in analyzer.GetAllWordsSorted())
                            Console.WriteLine($"{wi.Word}\t{wi.Count}");
                        break;

                    case "4":
                        // Feature 6: longest word
                        var longest = analyzer.GetLongestWord();
                        if (longest != null)
                            Console.WriteLine($"Longest word: {longest.Word} ({longest.Count} occurrences)");
                        else
                            Console.WriteLine("No words analysed.");
                        break;

                    case "5":
                        // Feature 7: most frequent word
                        var mostFreq = analyzer.GetMostFrequentWord();
                        if (mostFreq != null)
                            Console.WriteLine($"Most frequent word: {mostFreq.Word} ({mostFreq.Count} occurrences)");
                        else
                            Console.WriteLine("No words analysed.");
                        break;

                    case "6":
                        // Feature 8: line numbers lookup
                        Console.Write("Enter word to look up line numbers: ");
                        string lookup = Console.ReadLine();
                        var lines = analyzer.GetLineNumbers(lookup);
                        if (lines.Count > 0)
                            Console.WriteLine($"\"{lookup}\" appears on line(s): {string.Join(", ", lines)}");
                        else
                            Console.WriteLine($"\"{lookup}\" not found.");
                        break;

                    case "7":
                        // Feature 9: frequency lookup
                        Console.Write("Enter word to look up frequency: ");
                        string lookupFreq = Console.ReadLine();
                        var info = analyzer.GetAllWordsSorted()
                                           .FirstOrDefault(wi =>
                                               string.Equals(wi.Word, lookupFreq, StringComparison.OrdinalIgnoreCase));
                        if (info != null)
                            Console.WriteLine($"\"{lookupFreq}\" occurs {info.Count} time(s).");
                        else
                            Console.WriteLine($"\"{lookupFreq}\" not found.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a number between 1 and 8.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
