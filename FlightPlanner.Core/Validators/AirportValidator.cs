using FlightPlanner.Core.Models;
using FluentValidation;

namespace FlightPlanner.Core.Validators
{
    public class AirportValidator : AbstractValidator<Airport>
    {
        public AirportValidator()
        {
            RuleFor(airport => airport.AirportCode).NotEmpty();
            RuleFor(airport => airport.City).NotEmpty();
            RuleFor(airport => airport.Country).NotEmpty();
        }
    }
}
