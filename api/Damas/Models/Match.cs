using Damas.Enums;
using Damas.Structs;

namespace Damas.Models
{
    public class Match
    {
        public Player PlayerOne { get; set; }
        public Guid PlayerOneId { get; set; }
        public Player PlayerTwo { get; set; }
        public Guid PlayerTwoId { get; set; }
        public Board Board { get; set; }
        public Guid BoardId { get; set; }
        public Player CurrentPlayer { get; set; }
        public Guid CurrentPlayerId { get; set; }
        public ICollection<Piece> Captures { get; set; }

        public Match(Player playerOne, Guid playerOneId, Player playerTwo, Guid playerTwoId, Board board, Guid boardId, Player currentPlayer, Guid currentPlayerId, ICollection<Piece> captures)
        {
            PlayerOne = playerOne;
            PlayerOneId = playerOneId;
            PlayerTwo = playerTwo;
            PlayerTwoId = playerTwoId;
            Board = board;
            BoardId = boardId;
            CurrentPlayer = currentPlayer;
            CurrentPlayerId = currentPlayerId;
            Captures = captures;
        }

        public IEnumerable<Piece> GetCurrentPlayerPieces()
        {
            return Board.Pieces.Where(piece => piece.Color == CurrentPlayer.Color);
        }

        public IEnumerable<Piece> GetOpponentPieces()
        {
            return Board.Pieces.Where(piece => piece.Color != CurrentPlayer.Color);
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

        public bool IsPositionOccupiedByCurrentPlayer(Position position)
        {
            return IsValidPosition(position) && Board.IsPositionOccupied(position, CurrentPlayer.Color);
        }

        public bool IsPositionOccupiedByOpponent(Position position)
        {
            return IsValidPosition(position) && Board.IsPositionOccupied(position, CurrentPlayer.Color.Opposite());
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

        public void CommitCaptures()
        {
            var pieces = Board.Pieces.Where(piece => piece.IsCaptured);

            foreach (var piece in pieces)
            {
                Captures.Add(piece);
                Board.RemovePiece(piece);
            }
        }
    }
}