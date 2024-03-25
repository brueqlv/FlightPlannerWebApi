using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.Delete
{
    public class DeleteFlightCommandHandler(IFlightService flightService)
        : IRequestHandler<DeleteFlightCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            var flightToDelete = flightService.GetById(request.Id);

            if (flightToDelete != null)
            {
                flightService.Delete(flightToDelete);
            }

            return new ServiceResult();
        }
    }
}
