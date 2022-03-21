using System.Linq.Expressions;
using Damas.Core.Enums;
using Damas.Data.Entities;

namespace Damas.Api.Models
{
    public class BasicMatchModel
    {
        public static Expression<Func<Match, BasicMatchModel>> Selector
        {
            get => (Match match) => FromEntity(match);
        }

        public static Func<Match, BasicMatchModel> FromEntity
        {
            get => (Match match) => new BasicMatchModel(
                match.Id,
                match.PlayerOneId,
                match.PlayerOneColor,
                match.PlayerTwoId,
                match.PlayerTwoColor,
                match.TurnColor,
                match.BoardSize,
                match.Pieces.Select(piece => new BasicPieceModel(
                    piece.Id,
                    piece.Position,
                    piece.Color,
                    piece.IsDama,
                    piece.IsCaptured
                )));
        }

        public Guid Id { get; set; }
        public Guid PlayerOneId { get; set; }
        public Color PlayerOneColor { get; set; }
        public Guid PlayerTwoId { get; set; }
        public Color PlayerTwoColor { get; set; }
        public Color TurnColor { get; set; }
        public BoardSize BoardSize { get; set; }
        public IEnumerable<BasicPieceModel> Pieces { get; set; }

        public BasicMatchModel(Guid id, Guid playerOneId, Color playerOneColor, Guid playerTwoId, Color playerTwoColor, Color turnColor, BoardSize boardSize, IEnumerable<BasicPieceModel> pieces)
        {
            Id = id;
            PlayerOneId = playerOneId;
            PlayerOneColor = playerOneColor;
            PlayerTwoId = playerTwoId;
            PlayerTwoColor = playerTwoColor;
            TurnColor = turnColor;
            BoardSize = boardSize;
            Pieces = pieces;
        }
    }
}