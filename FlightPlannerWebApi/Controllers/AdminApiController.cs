using FlightPlannerWebApi.Interfaces;
using FlightPlannerWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerWebApi.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _locker = new();
        private IFlightService _flightStorage;

        public AdminApiController(IFlightService flightStorage)
        {
            _flightStorage = flightStorage;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightStorage.GetFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
                if (!_flightStorage.IsFlightValid(flight))
                {
                    return BadRequest();
                }

                if (_flightStorage.FlightExists(flight))
                {
                    return Conflict();
                }

                var addedFlight = _flightStorage.AddFlight(flight);

                return Created("", addedFlight);
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            _flightStorage.DeleteFlightById(id);
            return Ok();
        }
    }
}
