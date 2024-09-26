using MediatR;
using TimesheetApp.Application.DTOs;

public record GetRegistrationsByDateAndEmployeeQuery(DateTime Date, string EmployeeId, string Auth0Id) : IRequest<IEnumerable<RegistrationDTO>>;
