using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        private static object _locker = new();

        public FlightService(IFlightDbContext context) : base(context)
        {
        }

        public Flight AddFlight(Flight flight)
        {
            lock (_locker)
            {
                var fromAirport = _dbContext.Airports.FirstOrDefault(a =>
                    a.AirportCode == flight.From.AirportCode);

                if (fromAirport == null)
                {
                    fromAirport = new Airport
                    {
                        AirportCode = flight.From.AirportCode,
                        City = flight.From.City,
                        Country = flight.From.Country
                    };

                    _dbContext.Airports.Add(fromAirport);
                }

                var toAirport = _dbContext.Airports.FirstOrDefault(a =>
                    a.AirportCode == flight.To.AirportCode);

                if (toAirport == null)
                {
                    toAirport = new Airport
                    {
                        AirportCode = flight.To.AirportCode,
                        City = flight.To.City,
                        Country = flight.To.Country
                    };

                    _dbContext.Airports.Add(toAirport);
                }

                var newFlight = new Flight
                {
                    Carrier = flight.Carrier,
                    ArrivalTime = flight.ArrivalTime,
                    DepartureTime = flight.DepartureTime,
                    From = fromAirport,
                    To = toAirport,
                };

                _dbContext.Flights.Add(newFlight);
                _dbContext.SaveChanges();

                return newFlight;
            }
        }

        public bool FlightExists(Flight flight)
        {
            lock (_locker)
            {
                return _dbContext.Flights.Any(f => f.Carrier.ToLower().Trim() == flight.Carrier.ToLower().Trim() &&
                                                   f.DepartureTime == flight.DepartureTime &&
                                                   f.ArrivalTime == flight.ArrivalTime &&
                                                   f.From.AirportCode.ToLower().Trim() ==
                                                   flight.From.AirportCode.ToLower().Trim());
            }
        }

        public Flight? GetFullFlightById(int id)
        {
            return _dbContext.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public List<Flight> SearchFlights(SearchFlightRequest request)
        {
            var fromAirportCode = request.From.ToUpper();
            var toAirportCode = request.To.ToUpper();

            var flights = _dbContext.Flights
                .Where(f =>
                    f.From.AirportCode.ToUpper() == fromAirportCode &&
                    f.To.AirportCode.ToUpper() == toAirportCode &&
                    (request.DepartureDate.Length < 16
                        ? f.DepartureTime.Substring(0, 10) == request.DepartureDate.Substring(0, 10)
                        : f.DepartureTime == request.DepartureDate))
                .ToList();

            return flights;
        }

        public PageResult GetPageResultByRequest(SearchFlightRequest request)
        {
            var searchedFlights = SearchFlights(request);

            return new PageResult(searchedFlights);
        }
    }
}
