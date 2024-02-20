using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerWebApi.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController : ControllerBase
    {
        private InMemoryFlightStorage _flightStorage = new InMemoryFlightStorage();
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightStorage.Clear();
            return Ok();
        }
    }
}
