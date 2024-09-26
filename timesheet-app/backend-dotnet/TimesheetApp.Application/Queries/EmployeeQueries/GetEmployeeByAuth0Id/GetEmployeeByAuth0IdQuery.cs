using MediatR;
using TimesheetApp.Domain.Models;

public record GetEmployeeByAuth0IdQuery(string Auth0Id) : IRequest<Employee>;
