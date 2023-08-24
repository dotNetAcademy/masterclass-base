using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Config
{
    public class TimesheetConfiguration : IEntityTypeConfiguration<Timesheet>
    {
        public void Configure(EntityTypeBuilder<Timesheet> modelBuilder)
        {
            modelBuilder.ToTable("Timesheet")
                .HasMany(t => t.Registrations)
                .WithOne();
        }
    }
}
