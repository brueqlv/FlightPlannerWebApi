using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService(IFlightDbContext context)
        : EntityService<Airport>(context), IAirportService
    {
        private static object _locker = new();

        public List<Airport> SearchAirports(string keyword)
        {
            lock (_locker)
            {
                keyword = keyword.ToLower().Trim();

                return _dbContext.Airports
                    .Where(a => a.City.ToLower().Contains(keyword) ||
                                                      a.Country.ToLower().Contains(keyword) ||
                                                      a.AirportCode.ToLower().Contains(keyword))
                    .ToList();
            }
        }
    }
}
