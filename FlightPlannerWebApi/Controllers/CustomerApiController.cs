using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.Airports.List;
using FlightPlanner.UseCases.Flights.Get;
using FlightPlanner.UseCases.Flights.List;
using FlightPlanner.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public async Task<IActionResult> SearchAirports(string search)
        {
            return (await mediator.Send(new GetAirportsListByKeywordQuery(search))).ToActionResult();
        }

        [HttpPost]
        [Route("flights/search")]
        public async Task<IActionResult> SearchFlights(SearchFlightRequest search)
        {
            return (await mediator.Send(new GetFlightsListBySearchFlightRequestQuery(search))).ToActionResult();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> FindFlightById(int id)
        {
            return (await mediator.Send(new GetFlightQuery(id))).ToActionResult();
        }
    }
}