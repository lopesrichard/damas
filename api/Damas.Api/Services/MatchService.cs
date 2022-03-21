using Damas.Api.Models;
using Damas.Api.Response;
using Damas.Core.Algorithms;
using Damas.Core.DataStructures;
using Damas.Core.Enums;
using Damas.Core.Exceptions;
using Damas.Core.Serialization;
using Damas.Core.Structs;
using Damas.Data;
using Damas.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Damas.Api.Services
{
    public class MatchService : IMatchService
    {
        private IApplicationDbContext _context;
        private IMoveCalculator _calculator;
        private IMoveSelector _selector;

        public MatchService(IApplicationDbContext context, IMoveCalculator calculator, IMoveSelector selector)
        {
            _context = context;
            _calculator = calculator;
            _selector = selector;
        }

        public async Task<IResult<BasicMatchModel>> GetMatch(Guid id)
        {
            var match = await _context.Matches
                .Where(match => match.Id == id)
                .Select(BasicMatchModel.Selector)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (match == null)
            {
                var message = new Message(MessageType.ERROR, $"Match {id} not found");
                return new Result<BasicMatchModel>(message);
            }

            return new Result<BasicMatchModel>(match);
        }

        public async Task<IResult<IEnumerable<BasicMoveModel>>> ListMoves(Guid id)
        {
            var match = await _context.Matches
                .Include(match => match.Moves.OrderByDescending(move => move.DateTime))
                .SingleOrDefaultAsync(match => match.Id == id);

            if (match == null)
            {
                var message = new Message(MessageType.ERROR, $"Match {id} not found");
                return new Result<IEnumerable<BasicMoveModel>>(message);
            }

            var moves = match.Moves.Select(BasicMoveModel.FromEntity);

            return new Result<IEnumerable<BasicMoveModel>>(moves);
        }

        public async Task<IResult<IEnumerable<NewMoveModel>>> ListPossibleMoves(Guid id)
        {
            var match = await _context.Matches
                .Include(match => match.Pieces)
                .SingleOrDefaultAsync(match => match.Id == id);

            if (match == null)
            {
                var message = new Message(MessageType.ERROR, $"Match {id} not found");
                return new Result<IEnumerable<NewMoveModel>>(message);
            }

            var model = Mapper.MatchToModel(match);

            var calculated = _calculator.Calculate(model);
            var selected = calculated.Select(_selector.Select);

            var moves = selected.SelectMany(move =>
            {
                var piece = match.Pieces.SingleOrDefault(piece => piece.Position == move.Root.Value);

                if (piece == null)
                {
                    throw new NullPieceException();
                }

                return move.Root.Children.Select(child =>
                {
                    var between = model.GetPositionsBetween(move.Root.Value, child.Value);

                    var capturedPiece = match.Pieces.SingleOrDefault(piece => piece.Color != match.TurnColor && between.Contains(piece.Position));

                    return new NewMoveModel(piece.Id, child.Value, capturedPiece?.Id, model.IsLastRow(child.Value));
                });
            });

            return new Result<IEnumerable<NewMoveModel>>(moves);
        }

        public async Task<IResult<BasicMatchModel>> NewMatch(NewMatchModel model)
        {
            var messages = new List<Message>();

            var playerOne = await _context.Players.FindAsync(model.PlayerOneId);
            var playerTwo = await _context.Players.FindAsync(model.PlayerTwoId);

            if (playerOne == null)
            {
                messages.Add(new Message(MessageType.ERROR, $"Player {model.PlayerOneId} not found"));
            }

            if (playerTwo == null)
            {
                messages.Add(new Message(MessageType.ERROR, $"Player {model.PlayerTwoId} not found"));
            }

            if (messages.Any())
            {
                return new Result<BasicMatchModel>(messages);
            }

            var match = new Match(Guid.Empty, model.PlayerOneId, model.PlayerOneColor, model.PlayerTwoId, model.PlayerTwoColor, Color.WHITE, model.BoardSize, null, false, DateTime.UtcNow, null);

            match.Pieces = CreatePieces();
            match.Moves = new List<Move>();

            _context.Matches.Add(match);

            await _context.SaveChanges();

            var data = BasicMatchModel.FromEntity(match);

            return new Result<BasicMatchModel>(data);
        }

        private ICollection<Piece> CreatePieces()
        {
            return new List<Piece>()
            {
                new Piece(Guid.Empty, Guid.Empty, new Position(0, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(2, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(4, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(6, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(1, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(3, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(5, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(7, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(0, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(2, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(4, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(6, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(1, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(3, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(5, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(7, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(0, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(2, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(4, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(6, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(1, 7), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(3, 7), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(5, 7), Color.BLACK, false, false),
                new Piece(Guid.Empty, Guid.Empty, new Position(7, 7), Color.BLACK, false, false),
            };
        }

        public async Task<IResult<BasicMatchModel>> UndoLastMove(Guid id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                var message = new Message(MessageType.ERROR, $"Match {id} not found");
                return new Result<BasicMatchModel>(message);
            }

            var move = await _context.Moves
                .Include(move => move.Piece)
                .Include(move => move.CapturedPiece)
                .Where(move => move.MatchId == match.Id)
                .OrderByDescending(move => move.DateTime)
                .FirstOrDefaultAsync();

            if (move == null)
            {
                var message = new Message(MessageType.ERROR, $"Match {id} has no moves");
                return new Result<BasicMatchModel>(message);
            }

            var piece = move.Piece;

            match.TurnColor = move.Piece.Color;
            piece.Position = move.PreviousPosition;

            if (move.CapturedPieceId.HasValue)
            {
                move.CapturedPiece.IsCaptured = false;
            }

            if (move.IsPromotionMove)
            {
                move.Piece.IsDama = false;
            }

            if (match.IsDraw)
            {
                match.IsDraw = false;
            }

            if (match.FinishedAt.HasValue)
            {
                match.FinishedAt = null;
            }

            if (match.WinnerId.HasValue)
            {
                match.WinnerId = null;
            }

            _context.Moves.Remove(move);

            await _context.SaveChanges();

            match.Pieces = await _context.Pieces
                .Where(move => move.MatchId == match.Id)
                .AsNoTracking()
                .ToListAsync();

            var model = BasicMatchModel.FromEntity(match);

            return new Result<BasicMatchModel>(model);
        }
    }
}
