using MediatR;
using TimesheetApp.Domain.Models;

public record GetRegistrationsQuery() : IRequest<IEnumerable<Registration>>;