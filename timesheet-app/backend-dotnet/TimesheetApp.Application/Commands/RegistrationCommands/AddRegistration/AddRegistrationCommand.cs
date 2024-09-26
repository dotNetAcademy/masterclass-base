using MediatR;
using TimesheetApp.Application.DTOs;

public record AddRegistrationCommand(string EmployeeId, AddRegistrationDTO AddRegistrationDTO) : IRequest;