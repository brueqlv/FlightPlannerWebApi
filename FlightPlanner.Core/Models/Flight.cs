using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightPlanner.Core.Models
{
    public class Flight : Entity
    {
        public int FromId { get; set; }

        [Required(ErrorMessage = "From airport is required.")]
        public Airport From { get; set; }

        public int ToId { get; set; }

        [Required(ErrorMessage = "To airport is required.")]
        public Airport To { get; set; }

        [Required(ErrorMessage = "Carrier is required.")]
        public string Carrier { get; set; }

        [Required(ErrorMessage = "Departure time is required.")]
        public string DepartureTime { get; set; }

        [Required(ErrorMessage = "Arrival time is required.")]
        public string ArrivalTime { get; set; }
    }
}
