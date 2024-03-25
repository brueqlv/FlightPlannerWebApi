using System.Net;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.List
{
    public class GetFlightsBySearchFlightRequestQueryHandler(IFlightService flightService) 
        : IRequestHandler<GetFlightsBySearchFlightRequestQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(GetFlightsBySearchFlightRequestQuery request, CancellationToken cancellationToken)
        {
            if (request.SearchFlightRequest.From == request.SearchFlightRequest.To)
            {
                return new ServiceResult
                {
                    Status = HttpStatusCode.BadRequest
                };
            }

            return new ServiceResult
            {
                ResultObject = flightService.GetPageResultByRequest(request.SearchFlightRequest),
                Status = HttpStatusCode.OK
            };
        }
    }
}
