using Damas.Api.Models;
using Damas.Api.Services;
using Damas.Data;
using Microsoft.AspNetCore.Mvc;

namespace Damas.Api.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListPlayers(
            [FromServices] IPlayerService service
        )
        {
            var result = await service.ListPlayers();

            if (!result.IsSuccess)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(
            [FromRoute] Guid id,
            [FromServices] IPlayerService service)
        {
            var result = await service.GetPlayer(id);

            if (!result.IsSuccess)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> NewPlayer(
            [FromBody] NewPlayerModel model,
            [FromServices] IPlayerService service,
            [FromServices] IApplicationDbContext context)
        {
            var result = await service.NewPlayer(model);

            if (!result.IsSuccess)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(
            [FromRoute] Guid id,
            [FromBody] UpdatePlayerModel model,
            [FromServices] IPlayerService service)
        {
            var result = await service.UpdatePlayer(id, model);

            if (!result.IsSuccess)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }
    }
}
