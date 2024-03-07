using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlannerWebApi.Models;
using FluentValidation;
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
        private readonly IValidator<Flight> _validator;

        public AdminApiController(IFlightService flightService, IMapper mapper, IValidator<Flight> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validator = validator;
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
            var validationResult = _validator.Validate(flightToAdd);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
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
