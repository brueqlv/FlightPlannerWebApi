using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.Get
{
    public record GetFlightQuery(int Id) : IRequest<ServiceResult>;
}
