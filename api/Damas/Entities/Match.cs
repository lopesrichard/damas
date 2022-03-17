using Damas.Enums;
using Damas.Structs;

namespace Damas.Entities
{
    public class Match : Entity
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

        public Match(Guid id, Player playerOne, Guid playerOneId, Player playerTwo, Guid playerTwoId, Board board, Guid boardId, Player currentPlayer, Guid currentPlayerId, ICollection<Piece> captures) : base(id)
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

        public bool IsPositionAvaialable(Position position)
        {
            return Board.IsValidPosition(position) && Board.IsPositionAvaialable(position);
        }

        public bool IsPositionOccupied(Position position)
        {
            return Board.IsValidPosition(position) && Board.IsPositionOccupied(position);
        }

        public bool IsPositionOccupiedByOpponent(Position position)
        {
            return Board.IsValidPosition(position) && Board.IsPositionOccupied(position, CurrentPlayer.Color.Opposite());
        }

        public Piece GetPieceAt(Position position)
        {
            return Board.GetPieceAt(position);
        }

        public void CapturePiece(Piece piece)
        {
            Captures.Add(piece);
            Board.RemovePiece(piece);
        }

        public void RestorePiece(Piece piece)
        {
            Captures.Remove(piece);
            Board.AddPiece(piece);
        }
    }
}