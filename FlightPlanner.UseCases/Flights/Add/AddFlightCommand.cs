using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.Add
{
    public record AddFlightCommand(AddFlightRequest AddFlightRequest) : IRequest<ServiceResult>;
}
