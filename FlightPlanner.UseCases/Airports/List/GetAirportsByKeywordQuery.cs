using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Airports.List
{
    public record GetAirportsByKeywordQuery(string Keyword) : IRequest<ServiceResult>;
}
