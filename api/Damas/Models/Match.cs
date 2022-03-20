using Damas.Enums;
using Damas.Structs;

namespace Damas.Models
{
    public class Match
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Color PlayerOneColor { get; set; }
        public Color PlayerTwoColor { get; set; }
        public Board Board { get; set; }
        public Color TurnColor { get; set; }

        public Match(Player playerOne, Color playerOneColor, Player playerTwo, Color playerTwoColor, Board board, Color turnColor)
        {
            PlayerOne = playerOne;
            PlayerOneColor = playerOneColor;
            PlayerTwoColor = playerTwoColor;
            PlayerTwo = playerTwo;
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
    }
}