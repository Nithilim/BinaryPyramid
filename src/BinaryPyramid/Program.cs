using System;
using System.IO;

namespace BinaryPyramid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter symmetrical binary tree text file path: ");
            string path = Console.ReadLine();
            if (File.Exists(path))
            {
                var tree = new SymmetricalBinaryTree();
                var binaryTree = tree.BuildTreeFromFile(path);
                var result = binaryTree.FindHighestSumPath();
                Console.WriteLine("Path: ");
                result.Path.ForEach(i => Console.Write($"{i.Data} "));
                Console.WriteLine($"\r\nSum is: {result.Sum()}");
            }
            else
                Console.WriteLine("file does not exist");
        }
    }
}
