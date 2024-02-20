using FlightPlannerWebApi.Interfaces;
using FlightPlannerWebApi.Models;

namespace FlightPlannerWebApi.Storage
{
    public class InMemoryFlightStorage : IFlightService
    {
        private static List<Flight> _flights = new();
        private static int _id;
        private static object _locker = new();

        public void AddFlight(Flight flight)
        {
            lock (_locker)
            {
                flight.Id = _id++;
                _flights.Add(flight);
            }
        }

        public PageResult GetPageResultByRequest(SearchFlightRequest request)
        {
            lock (_locker)
            {
                var searchedFlights = SearchFlights(request);

                return new PageResult(searchedFlights);
            }
        }

        public List<Flight> SearchFlights(SearchFlightRequest request)
        {
            var flights = new List<Flight>();
            DateTime requestDepartureDateTime = DateTime.Parse(request.DepartureDate);

            foreach (var flight in _flights)
            {
                DateTime flightDateTime = DateTime.Parse(flight.DepartureTime);
                if (string.Equals(flight.From.AirportCode, request.From, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(flight.To.AirportCode, request.To, StringComparison.OrdinalIgnoreCase))
                {
                    if ((request.DepartureDate.Length < 16 && flightDateTime.Date ==
                            requestDepartureDateTime.Date) || flightDateTime == requestDepartureDateTime)
                    {
                        flights.Add(flight);
                    }
                }
            }

            return flights;
        }

        public void DeleteFlightById(int id)
        {
            lock (_locker)
            {
                var flightToRemove = GetFlightById(id);

                if (flightToRemove != null)
                {
                    _flights.Remove(flightToRemove);
                }
            }
        }

        public Flight? GetFlightById(int id)
        {
            lock (_locker)
            {
                return _flights.FirstOrDefault(flight => flight.Id == id);
            }
        }

        public void Clear()
        {
            _flights.Clear();
        }

        public bool IsFlightValid(Flight flight)
        {
            if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
            {
                return false;
            }

            DateTime? arrivalDate = DateTime.Parse(flight.ArrivalTime);
            DateTime? departureDate = DateTime.Parse(flight.DepartureTime);

            if (departureDate >= arrivalDate)
            {
                return false;
            }

            return true;
        }

        public bool FlightExists(Flight flight)
        {
            lock (_locker)
            {
                return _flights.Any(f => f.Carrier.ToLower().Trim() == flight.Carrier.ToLower().Trim()
                                         && f.DepartureTime == flight.DepartureTime &&
                                         f.ArrivalTime == flight.ArrivalTime
                                         && f.From.AirportCode.ToLower().Trim() ==
                                         flight.From.AirportCode.ToLower().Trim()
                                         && flight.To.AirportCode.ToLower().Trim() ==
                                         flight.To.AirportCode.ToLower().Trim());
            }
        }

        public List<Airport> SearchAirports(string keyword)
        {
            keyword = keyword.ToLower().Trim();

            return GetAllAirports()
                .Where(airport => airport.City.ToLower().Contains(keyword) ||
                                  airport.Country.ToLower().Contains(keyword) ||
                                  airport.AirportCode.ToLower().Contains(keyword))
                .ToList();
        }

        public HashSet<Airport> GetAllAirports()
        {
            HashSet<Airport> airports = new();

            foreach (var flight in _flights)
            {
                airports.Add(flight.From);
                airports.Add(flight.To);
            }

            return airports;
        }
    }
}
