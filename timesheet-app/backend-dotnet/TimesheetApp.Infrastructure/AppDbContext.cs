using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TimesheetApp.Domain.Models;
using TimesheetApp.Infrastructure.MediatorExtension;

namespace TimesheetApp.Infrastructure;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        var result = await base.SaveChangesAsync();
        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Timesheet> Timesheets { get; set; }
    public DbSet<Holiday> Holidays { get; set; }

}
