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
            Root = new GeneralTreeNode<T>(root, 0, null);
        }

        public HashSet<GeneralTreeNode<T>> Nodes()
        {
            return Root.Nodes();
        }

        public HashSet<GeneralTreeNode<T>> Leaves()
        {
            return Root.Leaves();
        }

        public int Height()
        {
            return Root.Height();
        }
    }
}