using Damas.DataStructures;
using Damas.Entities;
using Damas.Exceptions;
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
            var directions = GetDirections(node.Value, (position) =>
           {
               return match.IsValidPosition(position);
           });

            foreach (var direction in directions)
            {
                var previous = node.Value;
                var append = false;

                foreach (var position in direction)
                {
                    var current = match.GetPieceAt(position);

                    if (current == null)
                    {
                        if (append)
                        {
                            var child = node.Append(position);

                            var backup = match.GetPieceAt(previous);

                            if (backup == null)
                            {
                                throw new NullPieceException();
                            }

                            piece.Position = position;
                            match.CapturePiece(backup);

                            CalculateCaptureMoves(match, piece, child);

                            piece.Position = node.Value;
                            match.RestorePiece(backup);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (current.IsCaptured)
                        {
                            continue;
                        }

                        if (append)
                        {
                            break;
                        }

                        if (current.Color == piece.Color)
                        {
                            break;
                        }

                        append = true;
                        previous = position;
                    }
                }
            }
        }

        private void CalculateNonCaptureMoves(Match match, GeneralTreeNode<Position> node)
        {
            var directions = GetDirections(node.Value, (position) =>
            {
                return match.IsValidPosition(position) && !match.IsPositionOccupied(position);
            });

            foreach (var direction in directions)
            {
                foreach (var position in direction)
                {
                    node.Append(position);
                }
            }
        }

        private IEnumerable<IEnumerable<Position>> GetDirections(Position initial, Func<Position, bool> condition)
        {
            yield return GetPositions(initial, condition, position => position.Northwest());
            yield return GetPositions(initial, condition, position => position.Northeast());
            yield return GetPositions(initial, condition, position => position.Southwest());
            yield return GetPositions(initial, condition, position => position.Southeast());
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