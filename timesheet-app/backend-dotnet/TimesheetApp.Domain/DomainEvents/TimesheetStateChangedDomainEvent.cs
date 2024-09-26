using MediatR;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.DomainEvents;

public class TimesheetStateChangedDomainEvent : INotification
{
    public Timesheet Timesheet { get; }

    public TimesheetStateChangedDomainEvent(Timesheet timesheet)
    {
        Timesheet = timesheet;
    }
}
