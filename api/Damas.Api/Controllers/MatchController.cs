using Damas.Api.Models;
using Damas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Damas.Api.Controllers
{
    [ApiController]
    [Route("matches")]
    public class MatchController : ControllerBase
    {
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
