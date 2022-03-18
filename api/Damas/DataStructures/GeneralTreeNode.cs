namespace Damas.DataStructures
{
    public class GeneralTreeNode<T>
    {
        public T Value { get; set; }
        public int Level { get; set; }

        public GeneralTreeNode<T>? Parent { get; set; }

        public List<GeneralTreeNode<T>> Children = new List<GeneralTreeNode<T>>();

        public GeneralTreeNode(T value, int level, GeneralTreeNode<T>? parent)
        {
            Value = value;
            Level = level;
            Parent = parent;
        }

        public GeneralTreeNode<T> Append(T child)
        {
            var node = new GeneralTreeNode<T>(child, Level + 1, this);
            Children.Add(node);
            return node;
        }
    }
}