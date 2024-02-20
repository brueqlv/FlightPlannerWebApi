using FlightPlannerWebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerWebApi.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController : ControllerBase
    {
        private IFlightService _flightStorage;

        public CleanupApiController(IFlightService flightStorage)
        {
            _flightStorage = flightStorage;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightStorage.Clear();
            return Ok();
        }
    }
}
