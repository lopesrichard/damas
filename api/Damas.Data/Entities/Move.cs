using Damas.Structs;

namespace Damas.Data.Entities
{
    public class Move : Entity
    {
        public Guid PieceId { get; set; }
        public Position PreviousPosition { get; set; }
        public Position NewPosition { get; set; }
        public Guid? CapturedPieceId { get; set; }
        public bool IsPromotionMove { get; set; }
        public Piece? Piece { get; set; }
        public Piece? CapturedPiece { get; set; }

        public Move(Guid id, Guid pieceId, Position previousPosition, Position newPosition, Guid? capturedPieceId, bool isPromotionMove) : base(id)
        {
            PieceId = pieceId;
            PreviousPosition = previousPosition;
            NewPosition = newPosition;
            CapturedPieceId = capturedPieceId;
            IsPromotionMove = isPromotionMove;
        }
    }
}