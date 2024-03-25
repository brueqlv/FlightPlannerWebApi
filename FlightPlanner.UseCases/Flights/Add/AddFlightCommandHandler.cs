using System.Net;
using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using FluentValidation;
using MediatR;

namespace FlightPlanner.UseCases.Flights.Add
{
    public class AddFlightCommandHandler(IFlightService flightService, IMapper mapper, IValidator<Flight> validator)
        : IRequestHandler<AddFlightCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddFlightCommand request, CancellationToken cancellationToken)
        {
            var flightToAdd = mapper.Map<Flight>(request.AddFlightRequest);
            var validationResult = await validator.ValidateAsync(flightToAdd, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.BadRequest
                };
            }

            if (flightService.FlightExists(flightToAdd))
            {
                return new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.Conflict
                };
            }

            var addedFlight = flightService.AddFlight(flightToAdd);

            return new ServiceResult
            {
                ResultObject = mapper.Map<AddFlightResponse>(addedFlight),
                Status = HttpStatusCode.Created
            };
        }
    }
}
