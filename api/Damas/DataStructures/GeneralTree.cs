namespace Damas.DataStructures
{
    public class GeneralTree<T>
    {
        public GeneralTreeNode<T> Root { get; set; }

        public GeneralTree(GeneralTreeNode<T> root)
        {
            Root = root;
        }

        public GeneralTree(T root)
        {
            Root = new GeneralTreeNode<T>(root);
        }

        public HashSet<GeneralTreeNode<T>> Nodes()
        {
            var nodes = new HashSet<GeneralTreeNode<T>>() { Root };

            Nodes(Root, nodes);

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

        public int CountNodes()
        {
            return Nodes().Count();
        }

        public HashSet<GeneralTreeNode<T>> Leaves()
        {
            var leaves = new HashSet<GeneralTreeNode<T>>();

            Leaves(Root, leaves);

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

        public int CountLeaves()
        {
            return Leaves().Count();
        }
    }
}