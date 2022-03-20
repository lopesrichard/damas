using Damas.Entities;

namespace Damas.Validators
{
    public class PieceValidator
    {
        public IEnumerable<Exception> Validate(Board board, Piece piece)
        {
            return ValidatePosition(board, piece);
        }

        public IEnumerable<Exception> ValidatePosition(Board board, Piece piece)
        {
            var validator = new PositionValidator();
            return validator.Validate(board.Size, piece.Position);
        }
    }
}