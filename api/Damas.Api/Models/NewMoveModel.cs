using Damas.Core.Structs;

namespace Damas.Api.Models
{
    public class NewMoveModel
    {
        public Guid PieceId { get; set; }
        public Position NewPosition { get; set; }
        public Guid? CapturedPieceId { get; set; }
        public bool IsPromotionMove { get; set; }

        public NewMoveModel(Guid pieceId, Position newPosition, Guid? capturedPieceId, bool isPromotionMove)
        {
            PieceId = pieceId;
            NewPosition = newPosition;
            CapturedPieceId = capturedPieceId;
            IsPromotionMove = isPromotionMove;
        }
    }
}