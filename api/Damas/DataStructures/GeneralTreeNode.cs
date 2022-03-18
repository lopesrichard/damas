namespace Damas.DataStructures
{
    public class GeneralTreeNode<T> : IGeneralTreeNode<T>
    {
        public T Value { get; }

        public int Depth { get; }

        public IGeneralTreeNode<T>? Parent { get; }

        public IList<IGeneralTreeNode<T>> Children { get; } = new List<IGeneralTreeNode<T>>();

        public IList<IGeneralTreeNode<T>> Nodes { get => ComputeNodes(); }

        public IList<IGeneralTreeNode<T>> Leaves { get => ComputeLeaves(); }

        public int Height { get => ComputeHeight(); }

        public GeneralTreeNode(T value, int depth, GeneralTreeNode<T>? parent)
        {
            Value = value;
            Depth = depth;
            Parent = parent;
        }

        public IGeneralTreeNode<T> Append(T child)
        {
            var node = new GeneralTreeNode<T>(child, Depth + 1, this);
            Children.Add(node);
            return node;
        }

        private IList<IGeneralTreeNode<T>> ComputeNodes()
        {
            var nodes = new List<IGeneralTreeNode<T>>() { this };

            ComputeNodes(this, nodes);

            return nodes;
        }

        private void ComputeNodes(IGeneralTreeNode<T> node, IList<IGeneralTreeNode<T>> nodes)
        {
            foreach (var child in node.Children)
            {
                nodes.Add(child);
                ComputeNodes(child, nodes);
            }
        }

        private IList<IGeneralTreeNode<T>> ComputeLeaves()
        {
            var leaves = new List<IGeneralTreeNode<T>>();

            ComputeLeaves(this, leaves);

            return leaves;
        }

        private void ComputeLeaves(IGeneralTreeNode<T> node, IList<IGeneralTreeNode<T>> leaves)
        {
            if (node.Children.Count == 0)
            {
                leaves.Add(node);
            }

            foreach (var child in node.Children)
            {
                ComputeLeaves(child, leaves);
            }
        }

        private int ComputeHeight()
        {
            return Leaves.Max(leave => leave.Depth) - Depth;
        }
    }
}