using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
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
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public AdminApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AddFlightResponse>(flight));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            var flightToAdd = _mapper.Map<Flight>(request);

            if (!_flightService.IsFlightValid(flightToAdd))
            {
                return BadRequest();
            }

            if (_flightService.FlightExists(flightToAdd))
            {
                return Conflict();
            }

            var addedFlight = _flightService.AddFlight(flightToAdd);

            return Created("", _mapper.Map<AddFlightResponse>(addedFlight));
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var flightToDelete = _flightService.GetById(id);

            if (flightToDelete != null)
            {
                _flightService.Delete(flightToDelete);
            }

            return Ok();
        }
    }
}
