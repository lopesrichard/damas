namespace Damas.DataStructures
{
    public interface IGeneralTreeNode<T>
    {
        T Value { get; }

        int Depth { get; }

        IGeneralTreeNode<T>? Parent { get; }

        IReadOnlyCollection<IGeneralTreeNode<T>> Children { get; }

        IReadOnlyCollection<IGeneralTreeNode<T>> Nodes { get; }

        IReadOnlyCollection<IGeneralTreeNode<T>> Leaves { get; }

        int Height { get; }

        IGeneralTreeNode<T> Append(T child);

        IGeneralTreeNode<T> Append(IGeneralTreeNode<T> child);

        IGeneralTreeNode<T> Remove(IGeneralTreeNode<T> child);
    }
}