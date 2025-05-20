using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_final
{
    // This class is responsible for
    // 1. Reading the text file
    // 2. Splitting each line into words
    // 3. Adding the word to our tree class
    public class TextAnalyzer
    {
        // hole the words
        private BinarySearchTree tree;

        // Characters to split on (spaces, punctuation, brackets, etc.)
        private static readonly char[] Delimiters = new char[]
        {
            ' ', '\t', ',', '.', ';', ':', '?', '!', '(', ')',
            '[', ']', '\"', '\'', '-', '\r', '\n'
        };

        // ----- Constructor -----
        // Starting with an empty tree
        public TextAnalyzer()
        {
            tree = new BinarySearchTree();
        }


        // Reads the entire file, line by line, splits into words,
        // lower-cases each token, and inserts into the BST with its line number.

        /// <param name="filePath">Path to the .txt file to analyse.</param>
        public void AnalyseFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                var tokens = lines[i].Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);

                foreach (var raw in tokens)
                {
                    var word = raw.ToLower();
                    tree.Insert(word, i + 1);  
                }
            }
        }


        // Returns all words in alphabetical order with their counts and line numbers in a list
        public List<WordInfo> GetAllWordsSorted()
        {
            return tree.InOrder();
        }



        // Finds and returns the word with the highest occurrence count.
        // if two (or more) words share the same highest count,
        // the alphabetically earliest one will be returned.
        public WordInfo GetMostFrequentWord()
        {
            // list of all the words sorted
            var all = tree.InOrder();

            if (all.Count == 0)
                return null;

            // assuming the first word is the most frequent one
            WordInfo max = all[0];
            
            // updating the most frequent word in case we find a higher count value
            foreach (var wi in all)
            {
                if (wi.Count > max.Count)
                    max = wi;
            }
            return max;
        }


        // Finds and returns the longest distinct word.
        // If two words tie for length, this returns the one that appeared earlier in alphabetical order
        public WordInfo GetLongestWord()
        {
            // list of all the words sorted
            var all = tree.InOrder();

            if (all.Count == 0)
                return null;

            // assuming the first word is the longest one
            WordInfo longest = all[0];

            // updating the longest word in case we find a higher length value
            foreach (var wi in all)
            {
                if (wi.Word.Length > longest.Word.Length)
                    longest = wi;
            }
            return longest;
        }

        // Retrieves the list of line numbers where the specific word appears
        // (in case the user is looking for the line numbers that a specific word 
        // has been seen there)
        // <param name="word">The word to look up.</param>
        public List<int> GetLineNumbers(string word)
        {
            // list of all the words sorted
            var all = tree.InOrder();


            // looking through the list for the one word matches
            foreach (var wi in all)
            {
                if (string.Equals(wi.Word, word, StringComparison.OrdinalIgnoreCase))
                    return wi.LineNumbers;
            }

            // if never found a matching word, return an empty list
            return new List<int>();
        }


        /// Returns the number of distinct words found in the text.
        public int GetUniqueWordCount()
        {
            // just count how many WordInfo objects we have
            return tree.InOrder().Count;
        }


        /// Returns all words in “insertion” (pre-order) order.
        public List<WordInfo> GetAllWordsUnsorted()
        {
            // tree.PreOrder() returns a List<WordInfo> in the order nodes were first inserted
            return tree.PreOrder();
        }

    }
}
