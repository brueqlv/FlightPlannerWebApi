using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight? GetFullFlightById(int id);
        bool FlightExists(Flight flight);
        void Clear();
        PageResult GetPageResultByRequest(SearchFlightRequest request);
        Flight AddFlight(Flight flight);
    }
}
