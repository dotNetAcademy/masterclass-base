using MediatR;
using TimesheetApp.Application.DTOs;

public record GetEmployeesQuery() : IRequest<IEnumerable<EmployeeDTO>>;