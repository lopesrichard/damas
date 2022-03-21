using Damas.Api.Models;
using Damas.Api.Response;

namespace Damas.Api.Services
{
    public interface IMoveService
    {
        Task<IResult<BasicMoveModel>> NewMove(NewMoveModel model);
    }
}
