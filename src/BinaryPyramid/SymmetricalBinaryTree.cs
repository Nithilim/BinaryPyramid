using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BinaryPyramid
{
    public class SymmetricalBinaryTree : BinaryTree
    {
        public SymmetricalBinaryTree(Node root) : base(root)
        {
        }

        public SymmetricalBinaryTree() : base(null)
        {
        }

        public SymmetricalBinaryTree BuildTreeFromFile(string path)
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
                        var currentLayer = GetLevelContent(currentLevel, fileInfo).Select(num => new Node(num)).ToList();
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

                return new SymmetricalBinaryTree(rootNode);
            }

            return null;
        }

        public NodePath FindHighestSumPath()
        {
            var results = EnumerateNodePaths();
            var filteredResults = new List<NodePath>();
            foreach (var result in results)
            {
                bool isLastEven = RootNode.IsDataEven;
                var resultPaths = result.Path;
                foreach(var item in resultPaths)
                {
                    if (RootNode.Id == item.Id)
                        continue;

                    if (item.IsDataEven != isLastEven)
                    {
                        if ((resultPaths.IndexOf(item) + 1) == resultPaths.Count)
                            filteredResults.Add(result);

                        isLastEven = item.IsDataEven;
                    }
                    else
                        break;
                }
            }

            var sums = filteredResults.Select(p => p.Sum()).ToList();
            int maxSum = sums.Max();
            return filteredResults[sums.IndexOf(maxSum)];
        }

        private List<NodePath> EnumerateNodePaths()
        {
            var parentPaths = new List<NodePath>
            {
                new NodePath(new List<Node> { RootNode, RootNode.LeftChild }),
                new NodePath(new List<Node> { RootNode, RootNode.RightChild })
            };

            while (parentPaths[0].Path.Last().HasChildren)
            {
                parentPaths = GetPathsForParents(parentPaths);
            }

            return parentPaths;
        }

        private List<NodePath> GetPathsForParents(List<NodePath> paths)
        {
            var newPaths = new List<NodePath>();
            var pathsToUpdate = new List<Guid>();
            foreach (var nodePath in paths)
            {
                var nodeChildren = GetChildNodes(nodePath.Path.Last());
                if (nodeChildren.Any())
                {
                    pathsToUpdate.Add(nodePath.Id);
                    foreach (var child in nodeChildren)
                    {
                        var newPath = new NodePath(new List<Node>(nodePath.Path));
                        newPath.AddLayer(child);
                        newPaths.Add(newPath);
                    }
                }
            }

            int updateCounter = 0;
            foreach (var item in pathsToUpdate)
            {
                var path = paths.First(i => i.Id == item);
                path.UpdatePath(newPaths[updateCounter].Path);
                newPaths.RemoveAt(updateCounter);
                updateCounter++;
            }

            paths.AddRange(newPaths);
            return paths;
        }

        private List<Node> GetChildNodes(Node parentNode)
        {
            if (parentNode.HasChildren)
                return new List<Node> { parentNode.LeftChild, parentNode.RightChild };

            return new List<Node>();
        }

        private int GetLevelCountFromFile(FileInfo info)
        {
            int level = 0;
            using (var reader = new StreamReader(info.FullName))
            {
                while (reader.ReadLine() != null)
                    level++;

                return level;
            }
        }

        private List<int> GetLevelContent(int level, FileInfo info)
        {
            string line = "";
            using (var reader = new StreamReader(info.FullName))
            {
                for (int lineNumber = 1; lineNumber <= level; lineNumber++)
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
            foreach (string num in stringNumbers)
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
