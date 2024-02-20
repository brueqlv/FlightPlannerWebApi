using FlightPlannerWebApi.Interfaces;
using FlightPlannerWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private IFlightService _flightStorage;

        public CustomerApiController(IFlightService flightStorage)
        {
            _flightStorage = flightStorage;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = _flightStorage.SearchAirports(search);

            return Ok(airports);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightRequest search)
        {
            if (search.From == search.To)
            {
                return BadRequest();
            }

            var flights = _flightStorage.GetPageResultByRequest(search);

            return Ok(flights);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightStorage.GetFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
