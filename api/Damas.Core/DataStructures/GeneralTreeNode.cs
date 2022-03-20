using System.Collections.ObjectModel;

namespace Damas.Core.DataStructures
{
    public class GeneralTreeNode<T> : IGeneralTreeNode<T>
    {
        public T Value { get; }

        public int Depth { get; }

        public IGeneralTreeNode<T>? Parent { get; }

        public IReadOnlyCollection<IGeneralTreeNode<T>> Children { get => new ReadOnlyCollection<IGeneralTreeNode<T>>(_children); }

        public IReadOnlyCollection<IGeneralTreeNode<T>> Nodes { get => ComputeNodes(); }

        public IReadOnlyCollection<IGeneralTreeNode<T>> Leaves { get => ComputeLeaves(); }

        public int Height { get => ComputeHeight(); }

        private IList<IGeneralTreeNode<T>> _children { get; } = new List<IGeneralTreeNode<T>>();


        public GeneralTreeNode(T value, int depth, IGeneralTreeNode<T>? parent)
        {
            Value = value;
            Depth = depth;
            Parent = parent;
        }

        public IGeneralTreeNode<T> Append(T child)
        {
            var node = new GeneralTreeNode<T>(child, Depth + 1, this);
            _children.Add(node);
            return node;
        }

        public IGeneralTreeNode<T> Append(IGeneralTreeNode<T> child)
        {
            _children.Add(child);
            return child;
        }

        public IGeneralTreeNode<T> Remove(IGeneralTreeNode<T> child)
        {
            _children.Remove(child);
            return child;
        }

        private IReadOnlyCollection<IGeneralTreeNode<T>> ComputeNodes()
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

        private IReadOnlyCollection<IGeneralTreeNode<T>> ComputeLeaves()
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