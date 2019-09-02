using System;

namespace BinaryPyramid
{
    public class Node
    {
        public Guid Id { get; }

        public int Data { get; }

        public bool IsDataEven { get; }

        public Node LeftChild { get; private set; }

        public Node RightChild { get; private set; }

        public bool HasChildren { get; private set; }

        public Node(int data)
        {
            Data = data;
            IsDataEven = data % 2 == 0;
            Id = Guid.NewGuid();
            HasChildren = false;
        }

        public void SetSymmetricalChildren(Node leftData, Node rightData)
        {
            HasChildren = true;
            LeftChild = leftData;
            RightChild = rightData;
        }
    }
}
