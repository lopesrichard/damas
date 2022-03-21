using Damas.Core.Enums;
using Damas.Core.Exceptions;
using Damas.Core.Structs;

namespace Damas.Data.Entities
{
    public class Piece : Entity
    {
        public Guid MatchId { get; set; }
        public Position Position { get; set; }
        public Color Color { get; set; }
        public bool IsDama { get; set; }
        public bool IsCaptured { get; set; }

        public Match Match
        {
            get => _match ?? throw new NotIncludedException();
            set => _match = value;
        }

        private Match? _match;

        public Piece(Guid id, Guid matchId, Position position, Color color, bool isDama, bool isCaptured) : base(id)
        {
            MatchId = matchId;
            Position = position;
            Color = color;
            IsDama = isDama;
            IsCaptured = isCaptured;
        }
    }
}