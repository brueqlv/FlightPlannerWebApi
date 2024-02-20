using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightPlannerWebApi.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int FromId { get; set; }

        [Required(ErrorMessage = "From airport is required.")]
        public Airport From { get; set; }

        [JsonIgnore]
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
