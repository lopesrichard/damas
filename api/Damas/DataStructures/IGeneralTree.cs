namespace Damas.DataStructures
{
    public interface IGeneralTree<T>
    {
        IGeneralTreeNode<T> Root { get; }

        IList<IGeneralTreeNode<T>> Nodes { get; }

        IList<IGeneralTreeNode<T>> Leaves { get; }

        int Height { get; }
    }
}