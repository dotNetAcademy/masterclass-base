using MediatR;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Application.Commands.RegistrationCommands.EditRegistration;

public class EditRegistrationHandler : IRequestHandler<EditRegistrationCommand>
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public EditRegistrationHandler(IRegistrationRepository registrationRepository, IEmployeeRepository employeeRepository)
    {
        _registrationRepository = registrationRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task Handle(EditRegistrationCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetById(request.EmployeeId)
            ?? throw new KeyNotFoundException("Employee not found");

        var editRegDTO = request.RegistrationDTO;

        var timeSlot = new TimeSlot(editRegDTO.Start, editRegDTO.End);
        employee.UpdateRegistration(editRegDTO.Id, editRegDTO.RegistrationType, timeSlot);

        await _employeeRepository.Update(employee);
    }
}
