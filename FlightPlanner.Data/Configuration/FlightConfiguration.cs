using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightPlanner.Data.Configuration
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder
                .HasOne(f => f.From)
                .WithMany()
                .HasForeignKey(f => f.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(f => f.To)
                .WithMany()
                .HasForeignKey(f => f.ToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
