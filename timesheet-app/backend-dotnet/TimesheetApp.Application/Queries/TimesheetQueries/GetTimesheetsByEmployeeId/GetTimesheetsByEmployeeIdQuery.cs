using MediatR;
using TimesheetApp.Application.DTOs;

public record GetTimesheetsByEmployeeIdQuery(string EmployeeId) : IRequest<IEnumerable<TimesheetDTO>>;
