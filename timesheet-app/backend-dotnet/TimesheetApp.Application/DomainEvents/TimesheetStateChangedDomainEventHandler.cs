using MediatR;
using Microsoft.Extensions.Logging;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.DomainEvents;

public class TimesheetStateChangedDomainEventHandler : INotificationHandler<TimesheetStateChangedDomainEvent>
{
    private readonly ILogger<TimesheetStateChangedDomainEventHandler> _logger;

    public TimesheetStateChangedDomainEventHandler(ILogger<TimesheetStateChangedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(TimesheetStateChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"The timesheet with id {notification.Timesheet.Id} is {(notification.IsSubmitted ? "submitted"
            : notification.IsApproved ? "approved" : "")}");
        return Task.CompletedTask;
    }
}

public record TimesheetStateChangedDomainEvent(Timesheet Timesheet, bool IsSubmitted = false, bool IsApproved = false)
    : INotification
{
}
