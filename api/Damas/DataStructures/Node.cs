namespace Damas.DataStructures
{
    public class Node<T>
    {
        public T Value { get; set; }

        public List<Node<T>> Children = new List<Node<T>>();

        public Node(T value)
        {
            Value = value;
        }

        public Node<T> Append(T child)
        {
            var node = new Node<T>(child);
            Children.Add(node);
            return node;
        }
    }
}