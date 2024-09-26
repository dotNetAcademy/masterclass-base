using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Queries.TimesheetQueries.GetTimesheetsByEmployeeId;

public class GetTimesheetsByEmployeeIdHandler : IRequestHandler<GetTimesheetsByEmployeeIdQuery, IEnumerable<TimesheetDTO>>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public GetTimesheetsByEmployeeIdHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<IEnumerable<TimesheetDTO>> Handle(GetTimesheetsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _timesheetRepository.GetByUserId(request.EmployeeId);
        var timesheetsWithRegistrations =
            from timesheet in result
            where timesheet.Registrations.Count > 0
            select timesheet;

        return timesheetsWithRegistrations.Select(timesheet => ConvertToTimesheetDTOWithWeeks.MapToTimesheetDTOWithWeeks(timesheet));
    }
}
