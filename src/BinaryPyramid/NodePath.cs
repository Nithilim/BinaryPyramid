using System.Collections.Generic;
using System.Linq;
using System;

namespace BinaryPyramid
{
    public class NodePath
    {
        public Guid Id { get; }

        public List<Node> Path { get; private set; }

        public NodePath(List<Node> path)
        {
            Path = path;
            Id = Guid.NewGuid();
        }

        public NodePath()
        {
            Id = Guid.NewGuid();
        }

        public void UpdatePath(List<Node> newPath)
        {
            Path.Clear();
            Path.AddRange(newPath);
        }

        public void AddLayer(Node node)
        {
            Path.Add(node);
        }

        public int Sum()
        {
            var ints = Path.Select(n => n.Data).ToList();
            return ints.Sum();
        }
    }
}
