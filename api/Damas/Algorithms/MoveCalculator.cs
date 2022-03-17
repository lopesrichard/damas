using System.ComponentModel;
using System.Security.Authentication.ExtendedProtection;
using Damas.DataStructures;
using Damas.Entities;
using Damas.Enums;
using Damas.Serialization;
using Damas.Structs;

namespace Damas.Algorithms
{
    public class MoveCalculator
    {
        public IDictionary<Guid, Node<Position>> CalculateAvailableMoves(Match match)
        {
            var pieces = match.GetCurrentPlayerPieces();

            return pieces.ToList().ToDictionary(piece => piece.Id, piece => CalculatePieceAvailableMoves(match, piece));
        }

        private Node<Position> CalculatePieceAvailableMoves(Match match, Piece piece)
        {
            return piece.IsDama ? CalculateDamaAvailableMoves(match, piece) : CalculateRegularPieceAvailableMoves(match, piece);
        }

        private Node<Position> CalculateDamaAvailableMoves(Match match, Piece piece)
        {
            return null;
        }

        private Node<Position> CalculateRegularPieceAvailableMoves(Match match, Piece piece)
        {
            var root = new Node<Position>(piece.Position);

            CalculateRegularPieceCaptureAvailableMoves(match, piece, root);

            if (root.Children.Count > 0)
            {
                return root;
            }

            CalculateRegularPieceNonCaptureAvailableMoves(match, root);

            return root;
        }

        private void CalculateRegularPieceCaptureAvailableMoves(Match match, Piece piece, Node<Position> node)
        {
            CalculateRegularPieceCaptureAvailableMoves(match, piece, node, node.Value.Northwest(1), node.Value.Northwest(2));
            CalculateRegularPieceCaptureAvailableMoves(match, piece, node, node.Value.Northeast(1), node.Value.Northeast(2));
            CalculateRegularPieceCaptureAvailableMoves(match, piece, node, node.Value.Southwest(1), node.Value.Southwest(2));
            CalculateRegularPieceCaptureAvailableMoves(match, piece, node, node.Value.Southeast(1), node.Value.Southeast(2));
        }

        private void CalculateRegularPieceCaptureAvailableMoves(Match match, Piece piece, Node<Position> node, Position nearest1, Position nearest2)
        {
            if (match.IsPositionOccupiedByOpponent(nearest1) && match.IsPositionAvaialable(nearest2))
            {
                var child = node.Append(nearest2);

                var backup = match.GetPieceAt(nearest1);

                piece.Position = nearest2;
                match.CapturePiece(backup);

                CalculateRegularPieceCaptureAvailableMoves(match, piece, child);

                piece.Position = node.Value;
                match.RestorePiece(backup);
            }
        }

        private void CalculateRegularPieceNonCaptureAvailableMoves(Match match, Node<Position> node)
        {
            var position = node.Value;

            var nearest1 = match.CurrentPlayer.Color == Color.WHITE ? position.Northwest(1) : position.Southwest(1);
            var nearest2 = match.CurrentPlayer.Color == Color.WHITE ? position.Northeast(1) : position.Southeast(1);

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