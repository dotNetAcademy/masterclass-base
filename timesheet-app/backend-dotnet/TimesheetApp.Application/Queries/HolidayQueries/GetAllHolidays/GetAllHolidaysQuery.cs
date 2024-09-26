using MediatR;
using TimesheetApp.Application.DTOs;

public record GetAllHolidaysQuery() : IRequest<IEnumerable<HolidayDTO>>;
