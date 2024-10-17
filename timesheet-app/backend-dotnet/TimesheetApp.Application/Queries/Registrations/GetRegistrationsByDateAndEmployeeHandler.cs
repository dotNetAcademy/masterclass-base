using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Mappers;

namespace TimesheetApp.Application.Queries.Registrations;

public class GetRegistrationsByDateAndEmployeeHandler : IRequestHandler<GetRegistrationsByDateAndEmployeeQuery, IEnumerable<RegistrationDTO>>
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly ITimesheetRepository _timesheetRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetRegistrationsByDateAndEmployeeHandler(IRegistrationRepository registrationRepository, ITimesheetRepository timesheetRepository, IEmployeeRepository employeeRepository)
    {
        _registrationRepository = registrationRepository;
        _timesheetRepository = timesheetRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<RegistrationDTO>> Handle(GetRegistrationsByDateAndEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByAuth0Id(request.Auth0Id, cancellationToken)
            ?? throw new KeyNotFoundException($"There is no employee with Auth0Id {request.Auth0Id}");

        var registrations = await _registrationRepository.GetByDateAndEmployeeAsync(request.Date, employee.Id, cancellationToken);
        var timesheet = await _timesheetRepository.CheckIfTimesheetOfRegistrationsIsApproved(request.Date.Month, request.Date.Year, employee.Id, cancellationToken);

        var dtos = registrations.Select(r => RegistrationMapper.ToDto(r, timesheet.IsApproved, request.Auth0Id));
        return dtos;
    }
}

public record GetRegistrationsByDateAndEmployeeQuery(DateTime Date, string Auth0Id) : IRequest<IEnumerable<RegistrationDTO>>;

