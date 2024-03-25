using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.List
{
    public record GetFlightsBySearchFlightRequestQuery(SearchFlightRequest SearchFlightRequest) : IRequest<ServiceResult>;
}
