using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlannerWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public CustomerApiController(IFlightService flightService, IAirportService airportService, IMapper mapper)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = _airportService.SearchAirports(search);

            return Ok(_mapper.Map<List<AirportViewModel>>(airports));
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightRequest search)
        {
            if (search.From == search.To)
            {
                return BadRequest();
            }

            var flights = _flightService.GetPageResultByRequest(search);

            return Ok(flights);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AddFlightResponse>(flight));
        }
    }
}
