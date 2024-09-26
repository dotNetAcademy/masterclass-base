using MediatR;
using Microsoft.Extensions.Logging;

namespace TimesheetApp.Domain.DomainEvents;

public class TimesheetStateChangedDomainEventHandler : INotificationHandler<TimesheetStateChangedDomainEvent>
{
    private readonly ILogger<TimesheetStateChangedDomainEventHandler> _logger;

    public TimesheetStateChangedDomainEventHandler(ILogger<TimesheetStateChangedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(TimesheetStateChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var str = $"The timesheet with id {notification.Timesheet.Id} is approved";
        _logger.LogInformation(str);
        return Task.CompletedTask;
    }
}
