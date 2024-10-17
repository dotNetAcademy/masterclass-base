using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Interfaces.Validators;
using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.Enums;
using TimesheetApp.Domain.Models.ValueObjects;
using TimesheetApp.Domain.Validators;

namespace TimesheetApp.Application.Commands.Registrations;

public class AddRegistrationCommandHandler : IRequestHandler<AddRegistrationCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHolidayRepository _holidayRepository;

    public AddRegistrationCommandHandler(IEmployeeRepository employeeRepository, IHolidayRepository holidayRepository)
    {
        _employeeRepository = employeeRepository;
        _holidayRepository = holidayRepository;
    }

    public async Task Handle(AddRegistrationCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByAuth0Id(request.AddRegistrationDTO.Auth0Id, cancellationToken)
            ?? throw new KeyNotFoundException("Employee not found");

        Enum.TryParse(request.AddRegistrationDTO.RegistrationType, out RegistrationType registrationType);

        var newRegistration = new Registration(registrationType, new TimeSlot(request.AddRegistrationDTO.Start, request.AddRegistrationDTO.End));

        IRegistrationValidator registrationValidator = new RegistrationValidator();

        var allHolidays = await _holidayRepository.GetAll(cancellationToken);
        if (registrationValidator.CheckIfRegistrationOverlapsWithHoliday(request.AddRegistrationDTO.Start, allHolidays.ToList()))
        {
            throw new AppException("This registration overlaps with a holiday.");
        }

        employee.AddRegistration(newRegistration);
        await _employeeRepository.Update(employee, cancellationToken);
    }
}

public record AddRegistrationCommand(AddRegistrationDTO AddRegistrationDTO) : IRequest;

