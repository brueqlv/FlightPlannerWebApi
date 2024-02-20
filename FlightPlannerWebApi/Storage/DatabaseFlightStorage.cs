using FlightPlannerWebApi.Interfaces;
using FlightPlannerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerWebApi.Storage
{
    public class DatabaseFlightStorage : IFlightService
    {
        private FlightDbContext _dbContext;
        private static object _locker = new();

        public DatabaseFlightStorage(FlightDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public void Clear()
        {
            lock (_locker)
            {
                _dbContext.Flights.RemoveRange(_dbContext.Flights);
                _dbContext.Airports.RemoveRange(_dbContext.Airports);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteFlightById(int id)
        {
            var flightToRemove = _dbContext.Flights.SingleOrDefault(f => f.Id == id);

            if (flightToRemove != null)
            {
                _dbContext.Remove(flightToRemove);
                _dbContext.SaveChanges();
            }
        }

        public bool FlightExists(Flight flight)
        {
            lock (_locker)
            {
                return _dbContext.Flights.Any(f => f.Carrier.ToLower().Trim() == flight.Carrier.ToLower().Trim() &&
                                                   f.DepartureTime == flight.DepartureTime &&
                                                   f.ArrivalTime == flight.ArrivalTime &&
                                                   f.From.AirportCode.ToLower().Trim() == flight.From.AirportCode.ToLower().Trim());
            }
        }

        public PageResult GetPageResultByRequest(SearchFlightRequest request)
        {
            var searchedFlights = SearchFlights(request);

            return new PageResult(searchedFlights);
        }

        public Flight? GetFlightById(int id)
        {
            lock (_locker)
            {
                return _dbContext.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .FirstOrDefault(f => f.Id == id);
            }
        }

        public bool IsFlightValid(Flight flight)
        {
            if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
            {
                return false;
            }

            var arrivalDate = DateTime.Parse(flight.ArrivalTime);
            var departureDate = DateTime.Parse(flight.DepartureTime);

            if (departureDate >= arrivalDate)
            {
                return false;
            }

            return true;
        }

        public List<Airport> SearchAirports(string keyword)
        {
            keyword = keyword.ToLower().Trim();

            return _dbContext.Airports.Where(a => a.City.ToLower().Contains(keyword) ||
                                                  a.Country.ToLower().Contains(keyword) ||
                                                  a.AirportCode.ToLower().Contains(keyword))
                .ToList();
        }

        public List<Flight> SearchFlights(SearchFlightRequest request)
        {
            var fromAirportCode = request.From.ToUpper();
            var toAirportCode = request.To.ToUpper();

            var flights = _dbContext.Flights
                .Where(f =>
                    f.From.AirportCode.ToUpper() == fromAirportCode &&
                    f.To.AirportCode.ToUpper() == toAirportCode &&
                    (request.DepartureDate.Length < 16 ?
                        f.DepartureTime.Substring(0, 10) == request.DepartureDate.Substring(0, 10) :
                        f.DepartureTime == request.DepartureDate))
                .ToList();

            return flights;
        }
    }
}
