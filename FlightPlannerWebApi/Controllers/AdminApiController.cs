using FlightPlanner.UseCases.Flights.Add;
using FlightPlanner.UseCases.Flights.Delete;
using FlightPlanner.UseCases.Flights.Get;
using FlightPlanner.UseCases.Models;
using FlightPlanner.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.WebApi.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            return (await mediator.Send(new GetFlightQuery(id))).ToActionResult();
        }

        [HttpPut]
        [Route("flights")]
        public async Task<IActionResult> AddFlight(AddFlightRequest request)
        {
            return (await mediator.Send(new AddFlightCommand(request))).ToActionResult();
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            return (await mediator.Send(new DeleteFlightCommand(id))).ToActionResult();
        }
    }
}
