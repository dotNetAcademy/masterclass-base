using MediatR;
using TimesheetApp.Application.DTOs;

public record AddHolidayCommand(HolidayDTO HolidayDTO) : IRequest;