namespace Damas.DataStructures
{
    public class GeneralTreeNode<T>
    {
        public T Value { get; set; }

        public List<GeneralTreeNode<T>> Children = new List<GeneralTreeNode<T>>();

        public GeneralTreeNode(T value)
        {
            Value = value;
        }

        public GeneralTreeNode<T> Append(T child)
        {
            var node = new GeneralTreeNode<T>(child);
            Children.Add(node);
            return node;
        }
    }
}