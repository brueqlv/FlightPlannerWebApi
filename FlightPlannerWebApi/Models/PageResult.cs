namespace FlightPlannerWebApi.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }

        public PageResult(List<Flight> flights)
        {
            Page = 0;
            TotalItems = flights.Count;
            Items = flights;
        }
    }
}
