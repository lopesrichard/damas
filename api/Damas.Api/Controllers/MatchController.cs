using Damas.Api.Models;
using Damas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Damas.Api.Controllers
{
    [ApiController]
    [Route("matches")]
    public class MatchController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatch(
            [FromRoute] Guid id,
            [FromServices] IMatchService service
        )
        {
            var result = await service.GetMatch(id);

            if (!result.IsSuccess)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> NewMatch(
            [FromBody] NewMatchModel model,
            [FromServices] IMatchService service
        )
        {
            var result = await service.NewMatch(model);

            if (!result.IsSuccess)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }
    }
}
