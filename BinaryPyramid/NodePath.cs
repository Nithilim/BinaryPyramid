using System.Collections.Generic;
using System.Linq;

namespace BinaryPyramid
{
    public class NodePath
    {
        public List<Node> Path { get; }

        public NodePath(List<Node> path)
        {
            Path = path;
        }

        public int Sum()
        {
            var ints = Path.Select(n => n.Data).ToList();
            return ints.Sum();
        }
    }
}
