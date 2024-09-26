using MediatR;
using TimesheetApp.Application.DTOs;

public record GetHolidaysByDateQuery(DateTime Date) : IRequest<IEnumerable<HolidayDTO>>;
