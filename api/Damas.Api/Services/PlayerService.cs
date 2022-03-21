using Damas.Api.Models;
using Damas.Api.Response;
using Damas.Data;
using Damas.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Damas.Api.Services
{
    public class PlayerService : IPlayerService
    {
        private IApplicationDbContext _context;

        public PlayerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IListResult<BasicPlayerModel>> ListPlayers()
        {
            var players = await _context.Players
                .Select(BasicPlayerModel.Selector)
                .AsNoTracking()
                .ToListAsync();

            return new ListResult<BasicPlayerModel>(players);
        }

        public async Task<IResult<BasicPlayerModel>> GetPlayer(Guid id)
        {
            var player = await _context.Players
                .Where(player => player.Id == id)
                .Select(BasicPlayerModel.Selector)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (player == null)
            {
                var message = new Message(MessageType.ERROR, $"Player {id} not found");
                return new Result<BasicPlayerModel>(message);
            }

            return new Result<BasicPlayerModel>(player);
        }

        public async Task<IResult<BasicPlayerModel>> NewPlayer(NewPlayerModel model)
        {
            var player = new Player(Guid.Empty, model.Name);

            _context.Players.Add(player);

            await _context.SaveChanges();

            var data = BasicPlayerModel.FromEntity(player);

            return new Result<BasicPlayerModel>(data);
        }

        public async Task<IResult<BasicPlayerModel>> UpdatePlayer(Guid id, UpdatePlayerModel model)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                var message = new Message(MessageType.ERROR, $"Player {id} not found");
                return new Result<BasicPlayerModel>(message);
            }

            player.Name = model.Name;

            await _context.SaveChanges();

            var data = BasicPlayerModel.FromEntity(player);

            return new Result<BasicPlayerModel>(data);
        }
    }
}
