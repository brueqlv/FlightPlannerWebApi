using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight? GetFullFlightById(int id);
        bool FlightExists(Flight flight);
        PageResult GetPageResultByRequest(SearchFlightRequest request);
        Flight AddFlight(Flight flight);
    }
}
