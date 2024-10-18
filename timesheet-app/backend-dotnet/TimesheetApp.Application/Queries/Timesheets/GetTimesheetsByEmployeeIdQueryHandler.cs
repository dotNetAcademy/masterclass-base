using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Mappers;

namespace TimesheetApp.Application.Queries.Timesheets;

public class GetTimesheetsByEmployeeIdQueryHandler : IRequestHandler<GetTimesheetsByEmployeeIdQuery, IEnumerable<TimesheetDTO>>
{
    private readonly ITimesheetRepository _timesheetRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetTimesheetsByEmployeeIdQueryHandler(ITimesheetRepository timesheetRepository, IEmployeeRepository employeeRepository)
    {
        _timesheetRepository = timesheetRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<TimesheetDTO>> Handle(GetTimesheetsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByAuth0Id(request.Auth0Id, cancellationToken)
            ?? throw new KeyNotFoundException($"There is no employee with Auth0Id {request.Auth0Id}");
        var result = await _timesheetRepository.GetByUserId(employee.Id, cancellationToken);

        return result.Select(timesheet => timesheet.ToDto());
    }
}

public record GetTimesheetsByEmployeeIdQuery(string Auth0Id) : IRequest<IEnumerable<TimesheetDTO>>;
