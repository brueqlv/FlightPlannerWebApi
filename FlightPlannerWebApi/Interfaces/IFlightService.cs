using FlightPlannerWebApi.Models;

namespace FlightPlannerWebApi.Interfaces
{
    public interface IFlightService
    {
        Flight AddFlight(Flight flight);
        PageResult GetPageResultByRequest(SearchFlightRequest request);
        List<Flight> SearchFlights(SearchFlightRequest request);
        void DeleteFlightById(int id);
        Flight? GetFlightById(int id);
        void Clear();
        bool IsFlightValid(Flight flight);
        bool FlightExists(Flight flight);
        List<Airport> SearchAirports(string keyword);
    }
}
