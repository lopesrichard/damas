using Damas.Core.Structs;

namespace Damas.Api.Models
{
    public class BasicMoveModel
    {
        public Guid Id { get; set; }
        public Guid PieceId { get; set; }
        public Position PreviousPosition { get; set; }
        public Position NewPosition { get; set; }
        public Guid? CapturedPieceId { get; set; }
        public bool IsPromotionMove { get; set; }

        public BasicMoveModel(Guid id, Guid pieceId, Position previousPosition, Position newPosition, Guid? capturedPieceId, bool isPromotionMove)
        {
            Id = id;
            PieceId = pieceId;
            PreviousPosition = previousPosition;
            NewPosition = newPosition;
            CapturedPieceId = capturedPieceId;
            IsPromotionMove = isPromotionMove;
        }
    }
}