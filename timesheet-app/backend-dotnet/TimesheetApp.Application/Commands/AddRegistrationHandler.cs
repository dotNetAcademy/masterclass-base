using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Domain.Interfaces;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Application.Commands
{
    public class AddRegistrationHandler : IRequestHandler<AddRegistrationCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public AddRegistrationHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(AddRegistrationCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetById(request.EmployeeId)
                ?? throw new KeyNotFoundException("Employee not found");

            var addRegDTO = request.AddRegistrationDTO;
            var newRegistration = new Registration(addRegDTO.RegistrationType, new TimeSlot(addRegDTO.Start, addRegDTO.End));

            employee.AddRegistration(newRegistration);
            await _employeeRepository.Update(employee);
        }
    }
}
