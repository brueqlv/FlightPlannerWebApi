using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.Delete
{
    public record DeleteFlightCommand(int Id) : IRequest<ServiceResult>;
}
