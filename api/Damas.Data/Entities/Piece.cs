using Damas.Enums;
using Damas.Structs;

namespace Damas.Data.Entities
{
    public class Piece : Entity
    {
        public Position Position { get; set; }
        public Color Color { get; set; }
        public bool IsDama { get; set; }
        public bool IsCaptured { get; set; }

        public Piece(Guid id, Position position, Color color, bool isDama, bool isCaptured) : base(id)
        {
            Position = position;
            Color = color;
            IsDama = isDama;
            IsCaptured = isCaptured;
        }
    }
}