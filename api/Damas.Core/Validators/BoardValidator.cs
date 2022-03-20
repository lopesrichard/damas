using Damas.Core.Enums;
using Damas.Core.Exceptions;
using Damas.Core.Models;

namespace Damas.Core.Validators
{
    public class BoardValidator
    {
        public IEnumerable<Exception> Validate(Board board)
        {
            return ValidatePiecesCount(board)
                .Concat(ValidatePieces(board))
                .Concat(ValidatePiecesSamePosition(board));
        }

        public IEnumerable<Exception> ValidatePiecesCount(Board board)
        {
            var exceptions = new List<Exception>();

            var maxNumberOfPieces = board.Size == BoardSize.ONE_HUNDRED_SQUARES ? 20 : 12;

            var whitePiecesCount = board.Pieces.Where(piece => piece.Color == Color.WHITE).Count();
            var blackPiecesCount = board.Pieces.Where(piece => piece.Color == Color.BLACK).Count();

            if (whitePiecesCount == 0 || blackPiecesCount == 0)
            {
                exceptions.Add(new NotEnoughPiecesException());
            }

            if (whitePiecesCount > maxNumberOfPieces || blackPiecesCount > maxNumberOfPieces)
            {
                exceptions.Add(new TooManyPiecesException());
            }

            return exceptions;
        }

        public IEnumerable<Exception> ValidatePieces(Board board)
        {
            var validator = new PieceValidator();
            return board.Pieces.SelectMany(piece => validator.Validate(board, piece));
        }

        public IEnumerable<Exception> ValidatePiecesSamePosition(Board board)
        {
            var exceptions = new List<Exception>();

            if (board.Pieces.GroupBy(piece => piece.Position).Any(group => group.Count() > 1))
            {
                exceptions.Add(new PieceInSamePositionException());
            }

            return exceptions;
        }
    }
}