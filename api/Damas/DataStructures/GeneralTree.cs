namespace Damas.DataStructures
{
    public class GeneralTree<T> : IGeneralTree<T>
    {
        public IGeneralTreeNode<T> Root { get; }

        public IList<IGeneralTreeNode<T>> Nodes { get => ComputeNodes(); }

        public IList<IGeneralTreeNode<T>> Leaves { get => ComputeLeaves(); }

        public int Height { get => ComputeHeight(); }

        public GeneralTree(IGeneralTreeNode<T> root)
        {
            Root = root;
        }

        public GeneralTree(T root)
        {
            Root = new GeneralTreeNode<T>(root, 0, null);
        }

        private IList<IGeneralTreeNode<T>> ComputeNodes()
        {
            return Root.Nodes;
        }

        private IList<IGeneralTreeNode<T>> ComputeLeaves()
        {
            return Root.Leaves;
        }

        private int ComputeHeight()
        {
            return Root.Height;
        }
    }
}