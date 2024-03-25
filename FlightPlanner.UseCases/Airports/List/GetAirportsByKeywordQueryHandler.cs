using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Airports.List
{
    public class GetAirportsByKeywordQueryHandler(IAirportService airportService, IMapper mapper)
        : IRequestHandler<GetAirportsByKeywordQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(GetAirportsByKeywordQuery request, CancellationToken cancellationToken)
        {
            var airports = airportService.SearchAirports(request.Keyword);

            return new ServiceResult
            {
                ResultObject = mapper.Map<List<AirportViewModel>>(airports),
                Status = HttpStatusCode.OK
            };
        }
    }
}
