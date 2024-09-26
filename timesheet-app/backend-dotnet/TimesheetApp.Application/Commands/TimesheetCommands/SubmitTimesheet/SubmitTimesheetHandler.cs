using MediatR;
using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Commands.TimesheetCommands.SubmitTimesheet;

public class SubmitTimesheetHandler : IRequestHandler<SubmitTimesheetCommand>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public SubmitTimesheetHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task Handle(SubmitTimesheetCommand request, CancellationToken cancellationToken)
    {
        var timesheet = _timesheetRepository.GetById(request.Id).Result
            ?? throw new AppException($"There is no timesheet with id {request.Id}");

        timesheet.SubmitTimesheet();
        await _timesheetRepository.Update(timesheet);
    }
}
