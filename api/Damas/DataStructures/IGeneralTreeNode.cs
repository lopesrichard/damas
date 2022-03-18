namespace Damas.DataStructures
{
    public interface IGeneralTreeNode<T>
    {
        T Value { get; }

        int Depth { get; }

        IGeneralTreeNode<T>? Parent { get; }

        IList<IGeneralTreeNode<T>> Children { get; }

        IList<IGeneralTreeNode<T>> Nodes { get; }

        IList<IGeneralTreeNode<T>> Leaves { get; }

        int Height { get; }

        IGeneralTreeNode<T> Append(T child);
    }
}