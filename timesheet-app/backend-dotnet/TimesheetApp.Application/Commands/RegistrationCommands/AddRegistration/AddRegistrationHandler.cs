using MediatR;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Interfaces.Services;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.ValueObjects;
using TimesheetApp.Domain.Services;

namespace TimesheetApp.Application.Commands.RegistrationCommands.AddRegistration;

public class AddRegistrationHandler : IRequestHandler<AddRegistrationCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHolidayRepository _holidayRepository;

    public AddRegistrationHandler(IEmployeeRepository employeeRepository, IHolidayRepository holidayRepository)
    {
        _employeeRepository = employeeRepository;
        _holidayRepository = holidayRepository;
    }

    public async Task Handle(AddRegistrationCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetById(request.EmployeeId)
            ?? throw new KeyNotFoundException("Employee not found");

        var addRegDTO = request.AddRegistrationDTO;
        var newRegistration = new Registration(addRegDTO.RegistrationType, new TimeSlot(addRegDTO.Start, addRegDTO.End));

        IValidateRegistration validateRegistration = new ValidateRegistration(_holidayRepository);

        await employee.AddRegistration(newRegistration, validateRegistration);
        await _employeeRepository.Update(employee);
    }
}
