using System.Linq.Expressions;
using Damas.Core.Enums;
using Damas.Data.Entities;

namespace Damas.Api.Models
{
    public class BasicMatchModel
    {
        public static Expression<Func<Match, BasicMatchModel>> Selector
        {
            get => (Match match) => new BasicMatchModel(
                match.Id,
                match.PlayerOneId,
                match.PlayerOneColor,
                match.PlayerTwoId,
                match.PlayerTwoColor,
                match.TurnColor,
                match.BoardSize,
                match.WinnerId,
                match.IsDraw,
                match.StartedAt,
                match.FinishedAt,
                match.Moves.Count(),
                match.Pieces.Select(piece => new BasicPieceModel(
                    piece.Id,
                    piece.Position,
                    piece.Color,
                    piece.IsDama,
                    piece.IsCaptured
                )));
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
                match.WinnerId,
                match.IsDraw,
                match.StartedAt,
                match.FinishedAt,
                match.Moves.Count(),
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
        public Guid? WinnerId { get; set; }
        public bool IsDraw { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int Moves { get; set; }
        public IEnumerable<BasicPieceModel> Pieces { get; set; }

        public BasicMatchModel(
            Guid id,
            Guid playerOneId,
            Color playerOneColor,
            Guid playerTwoId,
            Color playerTwoColor,
            Color turnColor,
            BoardSize boardSize,
            Guid? winnerId,
            bool isDraw,
            DateTime startedAt,
            DateTime? finishedAt,
            int moves,
            IEnumerable<BasicPieceModel> pieces)
        {
            Id = id;
            PlayerOneId = playerOneId;
            PlayerOneColor = playerOneColor;
            PlayerTwoId = playerTwoId;
            PlayerTwoColor = playerTwoColor;
            TurnColor = turnColor;
            BoardSize = boardSize;
            WinnerId = winnerId;
            IsDraw = isDraw;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
            Moves = moves;
            Pieces = pieces;
        }
    }
}