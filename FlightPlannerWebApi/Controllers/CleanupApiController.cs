using FlightPlanner.UseCases.Cleanup;
using FlightPlanner.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.WebApi.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("clear")]
        public async Task<IActionResult> Clear()
        {
            return (await mediator.Send(new DataCleanupCommand())).ToActionResult();
        }
    }
}
