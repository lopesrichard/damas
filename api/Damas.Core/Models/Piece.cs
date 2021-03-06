using Damas.Core.Enums;
using Damas.Core.Structs;

namespace Damas.Core.Models
{
    public class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; set; }
        public bool IsDama { get; set; }
        public bool IsCaptured { get; set; }

        public Piece(Position position, Color color, bool isDama, bool isCaptured)
        {
            Position = position;
            Color = color;
            IsDama = isDama;
            IsCaptured = isCaptured;
        }
    }
}