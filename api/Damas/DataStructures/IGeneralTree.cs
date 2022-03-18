namespace Damas.DataStructures
{
    public interface IGeneralTree<T>
    {
        IGeneralTreeNode<T> Root { get; }

        IReadOnlyCollection<IGeneralTreeNode<T>> Nodes { get; }

        IReadOnlyCollection<IGeneralTreeNode<T>> Leaves { get; }

        int Height { get; }
    }
}