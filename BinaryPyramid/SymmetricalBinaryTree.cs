using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryPyramid
{
    public class SymmetricalBinaryTree : BinaryTree
    {
        public int TotalLevels { get; private set; }

        public SymmetricalBinaryTree(Node root) : base(root)
        {
        }

        public SymmetricalBinaryTree() : base(null)
        {
        }

        public BinaryTree BuildTreeFromFile(string path)
        {
            if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                int maxLevel = GetLevelCountFromFile(fileInfo);
                Node rootNode = default;
                var lastLayer = new List<Node>();
                if (maxLevel > 0)
                {
                    for (int currentLevel = 1; currentLevel <= maxLevel; currentLevel++)
                    {
                        var currentLayer = GetLevelContent(currentLevel, fileInfo).Select(num => new Node(num, currentLevel)).ToList();
                        if (currentLevel == 1)
                        {
                            rootNode = currentLayer[0];
                        }

                        else
                        {
                            foreach (Node node in lastLayer)
                            {
                                int pos = lastLayer.IndexOf(node);
                                node.SetSymmetricalChildren(currentLayer[pos], currentLayer[pos + 1]);
                            }
                        }

                        lastLayer = currentLayer;
                    }
                }

                return new BinaryTree(rootNode, maxLevel);
            }

            return null;
        }

        private int GetLevelCountFromFile(FileInfo info)
        {
            int level = 0;
            using(var reader = new StreamReader(info.FullName))
            {
                while (reader.ReadLine() != null)
                    level++;

                return level;
            }
        }

        private List<int> GetLevelContent(int level, FileInfo info)
        {
            string line = "";
            using(var reader = new StreamReader(info.FullName))
            {
                for(int lineNumber = 1; lineNumber <= level; lineNumber++)
                {
                    if (lineNumber == level)
                    {
                        line = reader.ReadLine();
                    }
                    else
                        reader.ReadLine();
                }
            }
            
            var stringNumbers = line.Split(" ");
            var result = new List<int>();
            foreach(string num in stringNumbers)
            {
                int number;
                bool parseResult = int.TryParse(num, out number);
                if (parseResult)
                    result.Add(number);
            }

            return result;
        }
    }
}
