using Damas.Api.Models;
using Damas.Api.Response;

namespace Damas.Api.Services
{
    public interface IMatchService
    {
        Task<IResult<BasicMatchModel>> NewMatch(NewMatchModel model);
    }
}
