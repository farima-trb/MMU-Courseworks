using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_final
{
    /// create a node for each unique word in the tree
    internal class Node
    {

        // hold the WordInfo object 
        private WordInfo data;

        private Node left;
        private Node right;


        public WordInfo Data
        {
            get { return data; }
        }

        // references to the node's Left and Right subtrees
        public Node Left
        {
            get { return left; }
            set { left = value; }
        }

        public Node Right
        {
            get { return right; }
            set { right = value; }
        }



        /// ----- Constructor -----
        public Node(WordInfo data)
        {
            this.data = data;
            this.left = null;
            this.right = null;
        }
    }

}
