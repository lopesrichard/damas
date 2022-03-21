using Damas.Api.Models;
using Damas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Damas.Api.Controllers
{
    [ApiController]
    [Route("moves")]
    public class MoveController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> NewMove(
            [FromBody] NewMoveModel model,
            [FromServices] IMoveService service
        )
        {
            var result = await service.NewMove(model);

            if (!result.IsSuccess)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }
    }
}
