using FlightPlanner.Core.Models;
using FlightPlanner.Core.Resources;
using FluentValidation;

namespace FlightPlanner.Core.Validators
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator()
        {
            RuleFor(flight => flight.Carrier).NotEmpty();

            RuleFor(flight => flight.ArrivalTime)
                .NotEmpty()
                .Must(ValidatorHelpers.BeValidDateTime);

            RuleFor(flight => flight.DepartureTime)
                .NotEmpty()
                .Must(ValidatorHelpers.BeValidDateTime)
                .Must((flight, departureTime) => ValidatorHelpers.IsDepartureBeforeArrival(flight.DepartureTime, flight.ArrivalTime));

            RuleFor(flight => flight.From.AirportCode)
                .Must((flight, fromAirportCode) =>
                {
                    var toAirportCode = flight.To?.AirportCode;

                    var notEqual = !string.Equals(fromAirportCode.Trim(), toAirportCode.Trim(), StringComparison.OrdinalIgnoreCase);

                    return notEqual;
                })
                .WithMessage(ValidationMessages.DepartureDestinationSameMessage);

            RuleFor(flight => flight.To).SetValidator(new AirportValidator());
            RuleFor(flight => flight.From).SetValidator(new AirportValidator());
        }
    }
}
