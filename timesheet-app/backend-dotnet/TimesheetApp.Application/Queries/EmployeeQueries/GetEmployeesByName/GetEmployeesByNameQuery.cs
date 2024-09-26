using MediatR;
using TimesheetApp.Application.DTOs;

public record GetEmployeesByNameQuery(string Name) : IRequest<IEnumerable<EmployeeDTO>>;
