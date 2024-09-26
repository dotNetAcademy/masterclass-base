using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Queries.RegistrationQueries.GetRegistrationsByDateAndEmployeeId;

public class GetRegistrationsByDateAndEmployeeHandler : IRequestHandler<GetRegistrationsByDateAndEmployeeQuery, IEnumerable<RegistrationDTO>>
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly ITimesheetRepository _timesheetRepository;

    public GetRegistrationsByDateAndEmployeeHandler(IRegistrationRepository registrationRepository, ITimesheetRepository timesheetRepository)
    {
        _registrationRepository = registrationRepository;
        _timesheetRepository = timesheetRepository;
    }

    public async Task<IEnumerable<RegistrationDTO>> Handle(GetRegistrationsByDateAndEmployeeQuery request, CancellationToken cancellationToken)
    {
        var registrations = await _registrationRepository.GetByDateAndEmployeeAsync(request.Date, request.EmployeeId);
        var timesheet = await _timesheetRepository.CheckIfTimesheetOfRegistrationsIsApproved(request.Date.Month, request.Date.Year, request.EmployeeId);
        var dtos = registrations.Select(r => ConvertToRegistrationDTO.MapRegistrationToDTO(r, timesheet.IsApproved, request.Auth0Id));
        return dtos;
    }
}
