using System.Text;
using Damas.Api.Models;
using Damas.Api.Response;
using Damas.Core.Algorithms;
using Damas.Core.Collections;
using Damas.Core.Enums;
using Damas.Core.Serialization;
using Damas.Data;
using Damas.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Damas.Api.Services
{
    public class MoveService : IMoveService
    {
        private IApplicationDbContext _context;
        private IMoveCalculator _calculator;
        private IMoveSelector _selector;

        public MoveService(IApplicationDbContext context, IMoveCalculator calculator, IMoveSelector selector)
        {
            _context = context;
            _calculator = calculator;
            _selector = selector;
        }

        public async Task<IResult<BasicMoveModel>> NewMove(NewMoveModel model)
        {
            var piece = await _context.Pieces.FindAsync(model.PieceId);

            if (piece == null)
            {
                var message = new Message(MessageType.ERROR, $"Player {model.PieceId} not found");
                return new Result<BasicMoveModel>(message);
            }

            if (piece.IsCaptured)
            {
                var message = new Message(MessageType.ERROR, $"Cannot move a captured piece");
                return new Result<BasicMoveModel>(message);
            }

            if (model.CapturedPieceId.HasValue)
            {
                var captured = await _context.Pieces.FindAsync(model.CapturedPieceId.Value);

                if (captured == null)
                {
                    var message = new Message(MessageType.ERROR, $"Player {model.CapturedPieceId.Value} not found");
                    return new Result<BasicMoveModel>(message);
                }

                captured.IsCaptured = true;
            }

            var match = await _context.Matches
                .Include(match => match.Pieces)
                .SingleOrDefaultAsync(match => match.Id == piece.MatchId);

            if (match == null)
            {
                var message = new Message(MessageType.ERROR, $"Match {piece.MatchId} not found");
                return new Result<BasicMoveModel>(message);
            }

            if (match.FinishedAt.HasValue)
            {
                var message = new Message(MessageType.ERROR, $"Match {piece.MatchId} aws already finished in {match.FinishedAt.Value}");
                return new Result<BasicMoveModel>(message);
            }

            if (piece.Color != match.TurnColor)
            {
                var message = new Message(MessageType.ERROR, $"It's {match.TurnColor} turn");
                return new Result<BasicMoveModel>(message);
            }

            var matchModel = Mapper.MatchToModel(match);

            var moves = _calculator.Calculate(Mapper.MatchToModel(match));
            var selecteds = moves.Select(_selector.Select);

            var selected = selecteds.SingleOrDefault(tree => tree.Root.Value == piece.Position);

            if (selected == null)
            {
                var message = new Message(MessageType.ERROR, $"Piece {model.PieceId} cannot move");
                return new Result<BasicMoveModel>(message);
            }

            var child = selected.Root.Children.SingleOrDefault(child => child.Value == model.NewPosition);

            if (child == null)
            {
                var content = new StringBuilder($"Piece {model.PieceId} cannot move to position {model.NewPosition.Serialize()}. ");

                if (selected.Root.Children.Count > 0)
                {
                    content.Append($"Positions available are: {string.Join(' ', selected.Root.Children.Select(child => child.Value.Serialize()))}");
                }

                var message = new Message(MessageType.ERROR, content.ToString());
                return new Result<BasicMoveModel>(message);
            }

            var isPromotionMove = matchModel.IsLastRow(model.NewPosition);

            if (isPromotionMove)
            {
                piece.IsDama = true;
            }

            var move = new Move(Guid.Empty, piece.MatchId, model.PieceId, piece.Position, model.NewPosition, model.CapturedPieceId, isPromotionMove, DateTime.UtcNow);

            _context.Moves.Add(move);

            if (child.Children.Count == 0)
            {
                match.TurnColor = match.TurnColor.Opposite();
            }

            if (match.Pieces.None(e => e.Color != piece.Color && !e.IsCaptured))
            {
                match.WinnerId = match.PlayerOneColor == piece.Color ? match.PlayerOneId : match.PlayerTwoId;
                match.FinishedAt = DateTime.UtcNow;
            }

            await _context.SaveChanges();

            var lastXMoves = _context.Moves
                .Where(move => move.MatchId == match.Id)
                .OrderByDescending(move => move.DateTime)
                .Take(match.BoardSize == BoardSize.SIXTY_FOUR_SQUARES ? 20 : 25);

            if (lastXMoves.All(move => !move.CapturedPieceId.HasValue && move.Piece.IsDama))
            {
                match.IsDraw = true;
                match.FinishedAt = DateTime.UtcNow;
            }

            await _context.SaveChanges();

            var data = BasicMoveModel.FromEntity(move);

            return new Result<BasicMoveModel>(data);
        }
    }
}
