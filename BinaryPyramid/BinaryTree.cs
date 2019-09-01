using System.Collections.Generic;
using System.Linq;

namespace BinaryPyramid
{
    public class BinaryTree
    {
        private readonly Node _rootNode;

        public int LayerCount { get; }

        public BinaryTree(Node rootNode)
        {
            _rootNode = rootNode;
        }

        public BinaryTree(Node rootNode, int layerCount)
        {
            _rootNode = rootNode;
            LayerCount = layerCount;
        }

        public NodePath FindHighestSumPath()
        {
            var result = EnumerateNodePaths();
            var paths = new List<NodePath>();
            foreach(var item in result)
            {
                paths.Add(new NodePath(item));
            }

            var sums = paths.Select(p => p.Sum()).ToList();
            int maxSum = sums.Max();
            return paths[sums.IndexOf(maxSum)];
        }

        private List<List<Node>> EnumerateNodePaths()
        {
            var parentPaths = new List<List<Node>>
            {
                new List<Node> { _rootNode, _rootNode.LeftChild },
                new List<Node> { _rootNode, _rootNode.RightChild }
            };

            while (parentPaths[0].Last().HasChildren)
            {
                parentPaths = GetPathsForParents(parentPaths);
            }

            return parentPaths;
        }

        private List<List<Node>> GetPathsForParents(List<List<Node>> paths)
        {
            var newPaths = new List<List<Node>>();
            var itemsToRemove = new List<List<Node>>();
            foreach (var nodeList in paths)
            {
                var nodeChildren = GetChildNodes(nodeList.Last());
                if (nodeChildren.Any())
                {
                    itemsToRemove.Add(nodeList);
                    foreach (var child in nodeChildren)
                    {
                        var temp = new List<Node>();
                        temp.AddRange(nodeList);
                        temp.Add(child);
                        newPaths.Add(temp);
                    }
                }
            }

            foreach(var item in itemsToRemove)
            {
                paths.RemoveAt(paths.IndexOf(item));
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
    }
}
