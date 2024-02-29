namespace FlightPlannerWebApi.Models
{
    public record SearchFlightRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
    }
}
