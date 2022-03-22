using Damas.Core.Structs;

namespace Damas.Api.Models
{
    public class NewMoveModel
    {
        public Guid PieceId { get; set; }
        public Position NewPosition { get; set; }

        public NewMoveModel(Guid pieceId, Position newPosition)
        {
            PieceId = pieceId;
            NewPosition = newPosition;
        }
    }
}