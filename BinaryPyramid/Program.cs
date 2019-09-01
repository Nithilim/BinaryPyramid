using System;

namespace BinaryPyramid
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new SymmetricalBinaryTree();
            var binaryTree = tree.BuildTreeFromFile(@"F:\uzduotis.txt");
            var result = binaryTree.FindHighestSumPath();
        }
    }
}
