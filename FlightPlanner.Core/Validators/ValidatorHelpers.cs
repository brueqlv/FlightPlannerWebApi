namespace FlightPlanner.Core.Validators
{
    public class ValidatorHelpers
    {
        public static bool BeValidDateTime(string arrivalTime)
        {
            if (!DateTime.TryParse(arrivalTime, out DateTime parsedDateTime))
            {
                return false;
            }

            return true;
        }

        public static bool IsDepartureBeforeArrival(string departureTime, string arrivalTime)
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
