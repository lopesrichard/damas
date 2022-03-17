using Damas.DataStructures;
using Damas.Entities;
using Damas.Structs;

namespace Damas.Algorithms
{
    public class DamaPieceMoveCalculator : IPieceMoveCalculator
    {
        public GeneralTree<Position> Calculate(Match match, Piece piece)
        {
            var tree = new GeneralTree<Position>(piece.Position);

            CalculateCaptureMoves(match, piece, tree.Root);

            if (tree.Root.Children.Count > 0)
            {
                return tree;
            }

            CalculateNonCaptureMoves(match, tree.Root);

            return tree;
        }

        private void CalculateCaptureMoves(Match match, Piece piece, GeneralTreeNode<Position> node)
        {
        }

        private void CalculateNonCaptureMoves(Match match, GeneralTreeNode<Position> node)
        {
            var position = node.Value;

            var append = (Func<Position, Position> move) =>
            {
                var condition = (Position position) =>
                {
                    return match.IsValidPosition(position) && !match.IsPositionOccupied(position);
                };

                for (var current = move(position); condition(current); current = move(current))
                {
                    if (match.IsPositionAvaialable(current))
                    {
                        node.Append(current);
                    }
                }
            };

            append(position => position.Northwest());
            append(position => position.Northeast());
            append(position => position.Southwest());
            append(position => position.Southeast());
        }
    }
}