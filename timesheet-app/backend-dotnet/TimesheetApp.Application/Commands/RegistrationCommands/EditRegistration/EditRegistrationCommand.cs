using MediatR;
using TimesheetApp.Application.DTOs;

public record EditRegistrationCommand(string EmployeeId, RegistrationDTO RegistrationDTO) : IRequest;