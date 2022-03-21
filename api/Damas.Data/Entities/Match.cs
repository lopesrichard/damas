using Damas.Core.Enums;
using Damas.Core.Exceptions;

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

        public Player PlayerOne
        {
            get => _playerOne ?? throw new NotIncludedException();
            set => _playerOne = value;
        }
        public Player PlayerTwo
        {
            get => _playerTwo ?? throw new NotIncludedException();
            set => _playerTwo = value;
        }
        public ICollection<Piece> Pieces
        {
            get => _pieces ?? throw new NotIncludedException();
            set => _pieces = value;
        }
        public ICollection<Move> Moves
        {
            get => _moves ?? throw new NotIncludedException();
            set => _moves = value;
        }

        private Player? _playerOne;
        private Player? _playerTwo;
        private ICollection<Piece>? _pieces;
        private ICollection<Move>? _moves;

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