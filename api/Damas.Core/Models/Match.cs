using Damas.Core.Enums;
using Damas.Core.Structs;

namespace Damas.Core.Models
{
    public class Match
    {
        public Color PlayerOneColor { get; set; }
        public Color PlayerTwoColor { get; set; }
        public Board Board { get; set; }
        public Color TurnColor { get; set; }

        public Match(Color playerOneColor, Color playerTwoColor, Board board, Color turnColor)
        {
            PlayerOneColor = playerOneColor;
            PlayerTwoColor = playerTwoColor;
            Board = board;
            TurnColor = turnColor;
        }

        public IEnumerable<Piece> GetPlayerPieces()
        {
            return Board.Pieces.Where(piece => piece.Color == TurnColor);
        }

        public IEnumerable<Piece> GetOpponentPieces()
        {
            return Board.Pieces.Where(piece => piece.Color != TurnColor);
        }

        public bool IsValidPosition(Position position)
        {
            return Board.IsValidPosition(position);
        }

        public bool IsPositionAvaialable(Position position)
        {
            return IsValidPosition(position) && Board.IsPositionAvaialable(position);
        }

        public bool IsPositionOccupied(Position position)
        {
            return IsValidPosition(position) && Board.IsPositionOccupied(position);
        }

        public bool IsPositionOccupiedByPlayer(Position position)
        {
            return IsValidPosition(position) && Board.IsPositionOccupied(position, TurnColor);
        }

        public bool IsPositionOccupiedByOpponent(Position position)
        {
            return IsValidPosition(position) && Board.IsPositionOccupied(position, TurnColor.Opposite());
        }

        public Piece? GetPieceAt(Position position)
        {
            return Board.GetPieceAt(position);
        }

        public void CapturePiece(Piece piece)
        {
            piece.IsCaptured = true;
        }

        public void RestorePiece(Piece piece)
        {
            piece.IsCaptured = false;
        }

        public bool IsLastRow(Position position)
        {
            var size = (double) Board.Size;
            var row = TurnColor == Color.WHITE ? Math.Sqrt(size) - 1 : 0;
            return position.Y == row;
        }
    }
}