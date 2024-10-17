using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Config;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> modelBuilder)
    {
        modelBuilder.Property(e => e.Role)
            .HasConversion<string>();

        modelBuilder.ToTable("Employee")
            .HasMany(e => e.Timesheets)
            .WithOne();
    }
}
