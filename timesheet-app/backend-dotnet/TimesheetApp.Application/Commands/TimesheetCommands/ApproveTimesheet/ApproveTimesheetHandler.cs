using MediatR;
using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Commands.TimesheetCommands.ApproveTimesheet;

public class ApproveTimesheetHandler : IRequestHandler<ApproveTimesheetCommand>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public ApproveTimesheetHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task Handle(ApproveTimesheetCommand request, CancellationToken cancellationToken)
    {
        var timesheet = _timesheetRepository.GetById(request.Id).Result
            ?? throw new AppException($"There is no timesheet with id {request.Id}");

        timesheet.ApproveTimesheet();
        await _timesheetRepository.Update(timesheet);
    }
}
