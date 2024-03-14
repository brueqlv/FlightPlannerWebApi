using FlightPlanner.Core.Models;
using FlightPlanner.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Data
{
    public class FlightDbContext : DbContext, IFlightDbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
        }
    }
}
