using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Cleanup
{
    public record DataCleanupCommand : IRequest<ServiceResult>;
}
