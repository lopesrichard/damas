using Damas.Core.Enums;
using Damas.Core.Structs;

namespace Damas.Api.Models
{
    public class BasicPieceModel
    {
        public Guid Id { get; set; }
        public Position Position { get; set; }
        public Color Color { get; set; }
        public bool IsDama { get; set; }
        public bool IsCaptured { get; set; }

        public BasicPieceModel(Guid id, Position position, Color color, bool isDama, bool isCaptured)
        {
            Id = id;
            Position = position;
            Color = color;
            IsDama = isDama;
            IsCaptured = isCaptured;
        }
    }
}