using Newtonsoft.Json;

namespace Damas.DataStructures
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GeneralTree<T> : IGeneralTree<T>
    {
        [JsonProperty]
        public IGeneralTreeNode<T> Root { get; }

        public IReadOnlyCollection<IGeneralTreeNode<T>> Nodes { get => Root.Nodes; }

        public IReadOnlyCollection<IGeneralTreeNode<T>> Leaves { get => Root.Leaves; }

        [JsonProperty]
        public int Height { get => Root.Height; }

        public GeneralTree(IGeneralTreeNode<T> root)
        {
            Root = root;
        }

        public GeneralTree(T root)
        {
            Root = new GeneralTreeNode<T>(root, 0, null);
        }
    }
}