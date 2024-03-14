using System.Net;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.List
{
    public class GetFlightsListBySearchFlightRequestQueryHandler(IFlightService flightService) 
        : IRequestHandler<GetFlightsListBySearchFlightRequestQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(GetFlightsListBySearchFlightRequestQuery request, CancellationToken cancellationToken)
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
