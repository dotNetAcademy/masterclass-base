using CSharpFunctionalExtensions;
using MediatR;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.MediatorExtension;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, AppDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Timesheet>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

        domainEntities.ToList().ForEach(e => e.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
