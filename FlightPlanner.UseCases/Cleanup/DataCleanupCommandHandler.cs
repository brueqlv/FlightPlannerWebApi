using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Cleanup
{
    public class DataCleanupCommandHandler(IDbService dbService)
        : IRequestHandler<DataCleanupCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DataCleanupCommand request, CancellationToken cancellationToken)
        {
            dbService.DeleteAll<Flight>();
            dbService.DeleteAll<Airport>();

            return new ServiceResult();
        }
    }
}
