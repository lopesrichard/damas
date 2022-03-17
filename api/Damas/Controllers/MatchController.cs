using Microsoft.AspNetCore.Mvc;

namespace Damas.Controllers;

[ApiController]
[Route("matches")]
public class MatchController : ControllerBase
{
    [HttpGet("/")]
    public void NewMatch()
    {
    }
}
