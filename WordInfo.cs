using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_final
{
    /// The data we need for one specific word

    public class WordInfo
    {
        private string word;
        private int count;
        private List<int> lineNumbers;

        // the string of the word itself
        public string Word
        {
            get { return word; }
        }

        // no. of the occurrence of the word
        public int Count
        {
            get { return count; }
        }

        // list of line numbers that a word has been seen
        public List<int> LineNumbers
        {
            get { return lineNumbers; }
        }

        /// ----- Constructor -----
        public WordInfo(string word, int lineNumber)
        {
            this.word = word;
            this.count = 1;
            this.lineNumbers = new List<int> { lineNumber };
        }

        // two things are recorded in this func:
        // 1. totall count of the word's occurrence
        // 2. the new line number if it hasn't been recorded
        public void AddOccurrence(int lineNumber)
        {
            // record the number of occurrence of the word
            // whether it's on the same line or a new line
            this.count++;

            // record each line number once, even if a word appears multiple times on the same line
            if (this.lineNumbers[this.lineNumbers.Count - 1] != lineNumber)
            {
                // Only when the last-recorded line is different, add the current line number into the list.
                this.lineNumbers.Add(lineNumber);
            }
        }
    }
}
