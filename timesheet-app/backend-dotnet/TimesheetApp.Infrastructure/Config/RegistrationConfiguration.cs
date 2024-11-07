using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Infrastructure.Config;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> modelBuilder)
    {
        modelBuilder.ToTable(nameof(Registration));
        modelBuilder.Property(r => r.RegistrationType)
            .HasConversion<string>();
        modelBuilder.OwnsOne(r => r.TimeSlot, ts =>
        {
            ts.Property(t => t.Start).HasColumnName(nameof(TimeSlot.Start));
            ts.Property(t => t.End).HasColumnName(nameof(TimeSlot.End));
        });
    }
}
