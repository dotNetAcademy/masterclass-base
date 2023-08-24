using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace TimesheetApp.Infrastructure.Config
{
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
}
