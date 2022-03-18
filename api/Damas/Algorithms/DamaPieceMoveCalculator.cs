using Damas.DataStructures;
using Damas.Entities;
using Damas.Exceptions;
using Damas.Structs;

namespace Damas.Algorithms
{
    public class DamaPieceMoveCalculator : IPieceMoveCalculator
    {
        public IGeneralTree<Position> Calculate(Match match, Piece piece)
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

        private void CalculateCaptureMoves(Match match, Piece piece, IGeneralTreeNode<Position> node)
        {
            var directions = GetDirections(node.Value, (position) =>
           {
               return match.IsValidPosition(position);
           });

            foreach (var direction in directions)
            {
                foreach (var position in direction.positions)
                {
                    if (match.IsPositionAvaialable(position))
                    {
                        continue;
                    }

                    if (match.IsPositionOccupiedByCurrentPlayer(position))
                    {
                        break;
                    }

                    var positions = GetPositions(position, (position) =>
                    {
                        return match.IsPositionAvaialable(position);
                    }, direction.move);

                    foreach (var current in positions)
                    {
                        var child = node.Append(current);

                        var backup = match.GetPieceAt(position);

                        if (backup == null)
                        {
                            throw new NullPieceException();
                        }

                        piece.Position = current;
                        match.CapturePiece(backup);

                        CalculateCaptureMoves(match, piece, child);

                        piece.Position = node.Value;
                        match.RestorePiece(backup);
                    }

                    break;
                }
            }
        }

        private void CalculateNonCaptureMoves(Match match, IGeneralTreeNode<Position> node)
        {
            var directions = GetDirections(node.Value, (position) =>
            {
                return match.IsValidPosition(position) && !match.IsPositionOccupied(position);
            });

            foreach (var direction in directions)
            {
                foreach (var position in direction.positions)
                {
                    node.Append(position);
                }
            }
        }

        private IEnumerable<(IEnumerable<Position> positions, Func<Position, Position> move)> GetDirections(Position initial, Func<Position, bool> condition)
        {
            yield return (GetPositions(initial, condition, position => position.Northwest()), position => position.Northwest());
            yield return (GetPositions(initial, condition, position => position.Northeast()), position => position.Northeast());
            yield return (GetPositions(initial, condition, position => position.Southwest()), position => position.Southwest());
            yield return (GetPositions(initial, condition, position => position.Southeast()), position => position.Southeast());
        }

        private IEnumerable<Position> GetPositions(Position initial, Func<Position, bool> condition, Func<Position, Position> move)
        {
            for (var current = move(initial); condition(current); current = move(current))
            {
                yield return current;
            }
        }
    }
}