using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.Get
{
    public class GetFlightQueryHandler(IFlightService flightService, IMapper mapper)
        : IRequestHandler<GetFlightQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(GetFlightQuery request, CancellationToken cancellationToken)
        {
            var flight = flightService.GetFullFlightById(request.Id);

            if (flight == null)
            {
                return new ServiceResult
                {
                    Status = HttpStatusCode.NotFound
                };
            }

            return new ServiceResult
            {
                ResultObject = mapper.Map<AddFlightResponse>(flight),
                Status = HttpStatusCode.OK
            };
        }
    }
}
