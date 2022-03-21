using Damas.Api.Models;
using Damas.Api.Response;

namespace Damas.Api.Services
{
    public interface IPlayerService
    {
        Task<IListResult<BasicPlayerModel>> ListPlayers();

        Task<IResult<BasicPlayerModel>> GetPlayer(Guid id);

        Task<IResult<BasicPlayerModel>> NewPlayer(NewPlayerModel model);

        Task<IResult<BasicPlayerModel>> UpdatePlayer(Guid id, UpdatePlayerModel model);
    }
}
