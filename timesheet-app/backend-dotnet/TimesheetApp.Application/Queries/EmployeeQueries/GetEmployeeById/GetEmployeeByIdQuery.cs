using MediatR;
using TimesheetApp.Domain.Models;

public record GetEmployeeByIdQuery(string Id) : IRequest<Employee>;