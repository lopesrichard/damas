using Damas.Core.Exceptions;
using Damas.Core.Structs;

namespace Damas.Data.Entities
{
    public class Move : Entity
    {
        public Guid MatchId { get; set; }
        public Guid PieceId { get; set; }
        public Position PreviousPosition { get; set; }
        public Position NewPosition { get; set; }
        public Guid? CapturedPieceId { get; set; }
        public bool IsPromotionMove { get; set; }
        public Piece? CapturedPiece { get; set; }
        public DateTime DateTime { get; set; }

        public Piece Piece
        {
            get => _piece ?? throw new NotIncludedException();
            set => _piece = value;
        }

        public Match Match
        {
            get => _match ?? throw new NotIncludedException();
            set => _match = value;
        }

        private Piece? _piece;
        private Match? _match;

        public Move(Guid id, Guid matchId, Guid pieceId, Position previousPosition, Position newPosition, Guid? capturedPieceId, bool isPromotionMove, DateTime dateTime) : base(id)
        {
            MatchId = matchId;
            PieceId = pieceId;
            PreviousPosition = previousPosition;
            NewPosition = newPosition;
            CapturedPieceId = capturedPieceId;
            IsPromotionMove = isPromotionMove;
            DateTime = dateTime;
        }
    }
}