using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Config;

public class TimesheetConfiguration : IEntityTypeConfiguration<Timesheet>
{
    public void Configure(EntityTypeBuilder<Timesheet> modelBuilder)
    {
        modelBuilder.ToTable("Timesheet")
            .HasMany(t => t.Registrations)
            .WithOne();
    }
}
