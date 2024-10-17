using MediatR;
using TimesheetApp.Application.DomainEvents;
using TimesheetApp.Application.Interfaces.Repositories;

namespace TimesheetApp.Application.Commands.Timesheets;

public class SubmitTimesheetCommandHandler : IRequestHandler<SubmitTimesheetCommand>
{
    private readonly ITimesheetRepository _timesheetRepository;
    private readonly IMediator _mediator;

    public SubmitTimesheetCommandHandler(ITimesheetRepository timesheetRepository, IMediator mediator)
    {
        _timesheetRepository = timesheetRepository;
        _mediator = mediator;
    }

    public async Task Handle(SubmitTimesheetCommand request, CancellationToken cancellationToken)
    {
        var timesheet = await _timesheetRepository.GetById(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"There is no timesheet with id {request.Id}");

        timesheet.SubmitTimesheet();
        await _timesheetRepository.Update(timesheet, cancellationToken);

        await _mediator.Publish(new TimesheetStateChangedDomainEvent(timesheet, IsSubmitted: true));
    }
}

public record SubmitTimesheetCommand(int Id) : IRequest;

