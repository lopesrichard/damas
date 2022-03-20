using Damas.Core.Enums;

namespace Damas.Data.Entities
{
    public class Match : Entity
    {
        public Guid PlayerOneId { get; set; }
        public Color PlayerOneColor { get; set; }
        public Guid PlayerTwoId { get; set; }
        public Color PlayerTwoColor { get; set; }
        public Color TurnColor { get; set; }
        public BoardSize BoardSize { get; set; }
        public Player? PlayerOne { get; set; }
        public Player? PlayerTwo { get; set; }
        public ICollection<Piece>? Pieces { get; set; }
        public ICollection<Move>? Moves { get; set; }

        public Match(Guid id, Guid playerOneId, Color playerOneColor, Guid playerTwoId, Color playerTwoColor, Color turnColor, BoardSize boardSize) : base(id)
        {
            PlayerOneId = playerOneId;
            PlayerOneColor = playerOneColor;
            PlayerTwoId = playerTwoId;
            PlayerTwoColor = playerTwoColor;
            TurnColor = turnColor;
            BoardSize = boardSize;
        }
    }
}