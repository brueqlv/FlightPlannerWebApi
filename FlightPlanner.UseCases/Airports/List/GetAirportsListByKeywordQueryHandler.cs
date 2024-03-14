using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Airports.List
{
    public class GetAirportsListByKeywordQueryHandler(IAirportService airportService, IMapper mapper)
        : IRequestHandler<GetAirportsListByKeywordQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(GetAirportsListByKeywordQuery request, CancellationToken cancellationToken)
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
