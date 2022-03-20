using Damas.Core.DataStructures;
using Damas.Core.Structs;

namespace Damas.Core.Algorithms
{
    public class MoveSelector : IMoveSelector
    {
        public IGeneralTree<Position> Select(IGeneralTree<Position> source)
        {
            var tree = new GeneralTree<Position>(source.Root.Value);

            Select(source.Root, tree.Root);

            return tree;
        }

        private void Select(IGeneralTreeNode<Position> source, IGeneralTreeNode<Position> node)
        {
            var height = source.Height - 1;

            foreach (var child in source.Children)
            {
                if (child.Height == height)
                {
                    Select(child, node.Append(child.Value));
                }
            }
        }
    }
}