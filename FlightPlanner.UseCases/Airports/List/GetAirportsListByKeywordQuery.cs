using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Airports.List
{
    public record GetAirportsListByKeywordQuery(string Keyword) : IRequest<ServiceResult>;
}
