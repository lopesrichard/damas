using Damas.Api.Models;
using Damas.Api.Response;
using Damas.Core.Enums;
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

        public MatchService(IApplicationDbContext context)
        {
            _context = context;
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

            var match = new Match(Guid.Empty, model.PlayerOneId, model.PlayerOneColor, model.PlayerTwoId, model.PlayerTwoColor, Color.WHITE, model.BoardSize);

            match.Pieces = CreatePieces();

            _context.Matches.Add(match);

            await _context.SaveChanges();

            var data = BasicMatchModel.FromEntity(match);

            return new Result<BasicMatchModel>(data);
        }

        private ICollection<Piece> CreatePieces()
        {
            return new List<Piece>()
            {
                new Piece(Guid.Empty, new Position(0, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(2, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(4, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(6, 0), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(1, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(3, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(5, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(7, 1), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(0, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(2, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(4, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(6, 2), Color.WHITE, false, false),
                new Piece(Guid.Empty, new Position(1, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(3, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(5, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(7, 5), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(0, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(2, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(4, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(6, 6), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(1, 7), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(3, 7), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(5, 7), Color.BLACK, false, false),
                new Piece(Guid.Empty, new Position(7, 7), Color.BLACK, false, false),
            };
        }
    }
}
