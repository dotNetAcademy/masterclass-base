using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Domain.Models.Enums;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Application.Commands.Registrations;

public class EditRegistrationCommandHandler : IRequestHandler<EditRegistrationCommand>
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public EditRegistrationCommandHandler(IRegistrationRepository registrationRepository, IEmployeeRepository employeeRepository)
    {
        _registrationRepository = registrationRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task Handle(EditRegistrationCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByAuth0Id(request.RegistrationDTO.Auth0Id!, cancellationToken)
            ?? throw new KeyNotFoundException("Employee not found");

        var timeSlot = new TimeSlot(request.RegistrationDTO.Start, request.RegistrationDTO.End);

        Enum.TryParse(request.RegistrationDTO.RegistrationType, out RegistrationType registrationType);

        employee.UpdateRegistration(request.RegistrationDTO.Id, registrationType, timeSlot);

        await _employeeRepository.Update(employee, cancellationToken);
    }
}

public record EditRegistrationCommand(RegistrationDTO RegistrationDTO) : IRequest;
