namespace Damas.DataStructures
{
    public class GeneralTreeNode<T>
    {
        public T Value { get; set; }
        public int Depth { get; set; }

        public GeneralTreeNode<T>? Parent { get; set; }

        public List<GeneralTreeNode<T>> Children = new List<GeneralTreeNode<T>>();

        public GeneralTreeNode(T value, int depth, GeneralTreeNode<T>? parent)
        {
            Value = value;
            Depth = depth;
            Parent = parent;
        }

        public GeneralTreeNode<T> Append(T child)
        {
            var node = new GeneralTreeNode<T>(child, Depth + 1, this);
            Children.Add(node);
            return node;
        }

        public HashSet<GeneralTreeNode<T>> Nodes()
        {
            var nodes = new HashSet<GeneralTreeNode<T>>() { this };

            Nodes(this, nodes);

            return nodes;
        }

        private void Nodes(GeneralTreeNode<T> node, HashSet<GeneralTreeNode<T>> nodes)
        {
            foreach (var child in node.Children)
            {
                nodes.Add(child);
                Nodes(child, nodes);
            }
        }

        public HashSet<GeneralTreeNode<T>> Leaves()
        {
            var leaves = new HashSet<GeneralTreeNode<T>>();

            Leaves(this, leaves);

            return leaves;
        }

        private void Leaves(GeneralTreeNode<T> node, HashSet<GeneralTreeNode<T>> leaves)
        {
            if (node.Children.Count == 0)
            {
                leaves.Add(node);
            }

            foreach (var child in node.Children)
            {
                Leaves(child, leaves);
            }
        }

        public int Height()
        {
            return Leaves().Max(leave => leave.Depth) - Depth;
        }
    }
}