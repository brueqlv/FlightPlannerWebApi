using FlightPlanner.Core.Models;
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
                .Must(BeValidDateTime);

            RuleFor(flight => flight.DepartureTime)
                .NotEmpty()
                .Must(BeValidDateTime)
                .Must((flight, departureTime) => IsDepartureBeforeArrival(flight.DepartureTime, flight.ArrivalTime));

            RuleFor(flight => flight.From.AirportCode)
                .Must((flight, fromAirportCode) =>
                {
                    var toAirportCode = flight.To?.AirportCode;

                    var notEqual = !string.Equals(fromAirportCode.Trim(), toAirportCode.Trim(), StringComparison.OrdinalIgnoreCase);

                    return notEqual;
                })
                .WithMessage("Departure and destination airports cannot be the same.");

            RuleFor(flight => flight.To).SetValidator(new AirportValidator());
            RuleFor(flight => flight.From).SetValidator(new AirportValidator());
        }

        private bool BeValidDateTime(string arrivalTime)
        {
            if (!DateTime.TryParse(arrivalTime, out DateTime parsedDateTime))
            {
                return false; 
            }

            return true;
        }

        private bool IsDepartureBeforeArrival(string departureTime, string arrivalTime)
        {
            if (!DateTime.TryParse(departureTime, out DateTime departureDateTime) ||
                !DateTime.TryParse(arrivalTime, out DateTime arrivalDateTime))
            {
                return false;
            }

            return departureDateTime < arrivalDateTime;
        }
    }
}
