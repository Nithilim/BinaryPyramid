using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryPyramid
{
    public class Node
    {
        public int Data { get; }

        public int Layer { get; }

        public Node LeftChild { get; private set; }

        public Node RightChild { get; private set; }

        public bool HasChildren { get; private set; }

        public Node(int data, int layer)
        {
            Data = data;
            Layer = layer;
            HasChildren = false;
        }

        public Node(int data, Node leftChild, Node rightChild)
        {
            Data = data;
            LeftChild = leftChild;
            RightChild = rightChild;
            HasChildren = true;
        }

        public void SetSymmetricalChildren(Node leftData, Node rightData)
        {
            HasChildren = true;
            LeftChild = leftData;
            RightChild = rightData;
        }
    }
}
