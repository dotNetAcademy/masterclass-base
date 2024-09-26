using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Config;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> modelBuilder)
    {
        modelBuilder.ToTable("Registration");

        modelBuilder.OwnsOne(r => r.TimeSlot)
                .Property(t => t.Start).HasColumnName("Start");
        modelBuilder.OwnsOne(r => r.TimeSlot)
                .Property(t => t.End).HasColumnName("End");
    }
}
