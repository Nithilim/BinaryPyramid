namespace BinaryPyramid
{
    public class Node
    {
        public int Data { get; }

        public Node LeftChild { get; private set; }

        public Node RightChild { get; private set; }

        public bool HasChildren { get; private set; }

        public Node(int data)
        {
            Data = data;
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
