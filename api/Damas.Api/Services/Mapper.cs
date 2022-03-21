namespace Damas.Api.Services
{
    public class Mapper
    {
        public static Damas.Core.Models.Match MatchToModel(Damas.Data.Entities.Match match)
        {
            var pieces = match.Pieces.Select(PieceToModel).ToHashSet();

            var board = new Damas.Core.Models.Board(match.BoardSize, pieces);

            return new Damas.Core.Models.Match(match.PlayerOneColor, match.PlayerTwoColor, board, match.TurnColor);
        }

        public static Damas.Core.Models.Piece PieceToModel(Damas.Data.Entities.Piece piece)
        {
            return new Damas.Core.Models.Piece(piece.Position, piece.Color, piece.IsDama, piece.IsCaptured);
        }
    }
}
