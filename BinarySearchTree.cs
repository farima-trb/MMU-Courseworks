using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_final
{
    /// Storing (inserting) and Organasing (in-order(alphabetically)) the WordInfo entries
    public class BinarySearchTree
    {
        private Node root;


        /// ----- Constructor -----
        // Create an empy tree 
        public BinarySearchTree()
        {
            root = null;
        }

        // take the word and its line number
        public void Insert(string word, int lineNumber)
        {
            root = Insert(root, word, lineNumber);
        }


        // Return a reference to a Node 
        private Node Insert(Node node, string word, int lineNumber)
        {
            // A new Node which is created for a word that wasn’t in the tree yet
            if (node == null)
                return new Node(new WordInfo(word, lineNumber));

            // applying a case-sensetive alphabeticall comparison between the strings of the
            // new insrted word and the 
            // word already stored at the current node
            int cmp = string.Compare(word, node.Data.Word, StringComparison.OrdinalIgnoreCase);
            
            // Alphabetically:
            // these two strings are equal
            if (cmp == 0)
                node.Data.AddOccurrence(lineNumber);
            // word comes before node.Data.Word in the sort order
            else if (cmp < 0)
                node.Left = Insert(node.Left, word, lineNumber);
            // word comes after node.Data.Word in the sort order
            else
                node.Right = Insert(node.Right, word, lineNumber);


            return node;
        }

        
        // Returns all WordInfo entries in alphabetical order.
        public List<WordInfo> InOrder()
        {
            var list = new List<WordInfo>();
            InOrder(root, list);
            return list;
        }

        private void InOrder(Node node, List<WordInfo> list)
        {
            if (node == null) return;
            InOrder(node.Left, list);
            list.Add(node.Data);
            InOrder(node.Right, list);
        }

        
        // Returns all WordInfo entries in pre-order (node, left, right).
        public List<WordInfo> PreOrder()
        {
            var list = new List<WordInfo>();
            PreOrder(root, list);
            return list;
        }

        private void PreOrder(Node node, List<WordInfo> list)
        {
            if (node == null) return;
            list.Add(node.Data);
            PreOrder(node.Left, list);
            PreOrder(node.Right, list);
        }


        // Returns all WordInfo entries in post-order (left, right, node).
        public List<WordInfo> PostOrder()
        {
            var list = new List<WordInfo>();
            PostOrder(root, list);
            return list;
        }

        private void PostOrder(Node node, List<WordInfo> list)
        {
            if (node == null) return;
            PostOrder(node.Left, list);
            PostOrder(node.Right, list);
            list.Add(node.Data);
        }
    }
}

