using Damas.DataStructures;
using Damas.Enums;
using Damas.Exceptions;
using Damas.Models;
using Damas.Structs;

namespace Damas.Algorithms
{
    public class RegularPieceMoveCalculator : IPieceMoveCalculator
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
            CalculateCaptureMoves(match, piece, node, node.Value.Northwest(1), node.Value.Northwest(2));
            CalculateCaptureMoves(match, piece, node, node.Value.Northeast(1), node.Value.Northeast(2));
            CalculateCaptureMoves(match, piece, node, node.Value.Southwest(1), node.Value.Southwest(2));
            CalculateCaptureMoves(match, piece, node, node.Value.Southeast(1), node.Value.Southeast(2));
        }

        private void CalculateCaptureMoves(Match match, Piece piece, IGeneralTreeNode<Position> node, Position nearest1, Position nearest2)
        {
            if (match.IsPositionOccupiedByOpponent(nearest1) && match.IsPositionAvaialable(nearest2))
            {
                var child = node.Append(nearest2);

                var backup = match.GetPieceAt(nearest1);

                if (backup == null)
                {
                    throw new NullPieceException();
                }

                piece.Position = nearest2;
                match.CapturePiece(backup);

                CalculateCaptureMoves(match, piece, child);

                piece.Position = node.Value;
                match.RestorePiece(backup);
            }
        }

        private void CalculateNonCaptureMoves(Match match, IGeneralTreeNode<Position> node)
        {
            var position = node.Value;

            var nearest1 = match.TurnColor == Color.WHITE ? position.Northwest(1) : position.Southwest(1);
            var nearest2 = match.TurnColor == Color.WHITE ? position.Northeast(1) : position.Southeast(1);

            if (match.IsPositionAvaialable(nearest1))
            {
                node.Append(nearest1);
            }

            if (match.IsPositionAvaialable(nearest2))
            {
                node.Append(nearest2);
            }
        }
    }
}