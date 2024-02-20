using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightPlannerWebApi.Models
{
    public class Airport
    {
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Airport code is required.")]
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}