using MediatR;
using TimesheetApp.Application.DomainEvents;
using TimesheetApp.Application.Interfaces.Repositories;

namespace TimesheetApp.Application.Commands.Timesheets;

public class ApproveTimesheetCommandHandler : IRequestHandler<ApproveTimesheetCommand>
{
    private readonly ITimesheetRepository _timesheetRepository;
    private readonly IMediator _mediator;

    public ApproveTimesheetCommandHandler(ITimesheetRepository timesheetRepository, IMediator mediator)
    {
        _timesheetRepository = timesheetRepository;
        _mediator = mediator;
    }

    public async Task Handle(ApproveTimesheetCommand request, CancellationToken cancellationToken)
    {
        var timesheet = _timesheetRepository.GetById(request.Id, cancellationToken).Result
            ?? throw new KeyNotFoundException($"There is no timesheet with id {request.Id}");

        timesheet.ApproveTimesheet();
        await _timesheetRepository.Update(timesheet, cancellationToken);

        await _mediator.Publish(new TimesheetStateChangedDomainEvent(timesheet, IsApproved: true));
    }
}

public record ApproveTimesheetCommand(int Id) : IRequest;

