using Damas.Core.DataStructures;
using Damas.Core.Structs;

namespace Damas.Core.Algorithms
{
    public class MoveSelector : IMoveSelector
    {
        public IEnumerable<IGeneralTree<Position>> Select(IEnumerable<IGeneralTree<Position>> source)
        {
            var moves = source
                .Where(tree => tree.Root.Children.Count > 0)
                .Select(tree => Select(tree));

            var captures = moves.Where(tree =>
            {
                var child = tree.Root.Children.ElementAt(0);
                return tree.Root.Value.Distance(child.Value) > 1;
            });

            return captures.Any() ? captures : moves;
        }

        private IGeneralTree<Position> Select(IGeneralTree<Position> source)
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