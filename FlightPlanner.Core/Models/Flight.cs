namespace FlightPlanner.Core.Models
{
    public class Flight : Entity
    {
        public int FromId { get; set; }

        public required Airport From { get; set; }

        public int ToId { get; set; }

        public required Airport To { get; set; }

        public required string Carrier { get; set; }

        public required string DepartureTime { get; set; }

        public required string ArrivalTime { get; set; }
    }
}
