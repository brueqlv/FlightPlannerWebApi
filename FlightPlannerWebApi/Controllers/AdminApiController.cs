using FlightPlannerWebApi.Models;
using FlightPlannerWebApi.Storage;
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
        private InMemoryFlightStorage _flightStorage = new InMemoryFlightStorage();

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
            lock (_locker)
            {
                if (!_flightStorage.IsFlightValid(flight))
                {
                    return BadRequest();
                }

                if (_flightStorage.FlightExists(flight))
                {
                    return Conflict();
                }

                _flightStorage.AddFlight(flight);

                return Created("", flight);
            }
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
