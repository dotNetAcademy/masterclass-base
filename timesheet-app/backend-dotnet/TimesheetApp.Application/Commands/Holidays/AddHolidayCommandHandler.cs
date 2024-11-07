using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Validators;

namespace TimesheetApp.Application.Commands.Holidays;

public class AddHolidayCommandHandler : IRequestHandler<AddHolidayCommand>
{
    private readonly IHolidayRepository _holidayRepository;

    public AddHolidayCommandHandler(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }

    public async Task Handle(AddHolidayCommand request, CancellationToken cancellationToken)
    {
        var validator = new HolidayValidator();

        var allHolidays = await _holidayRepository.GetAll(cancellationToken);
        if (validator.CheckIfThereIsAlreadyAHolidayRegistredOnDay(request.HolidayDTO.Date, allHolidays.ToList()))
        {
            throw new AppException("There is already a holiday registred on this day!");
        }

        var newHoliday = new Holiday(request.HolidayDTO.Date, request.HolidayDTO.Name);
        await _holidayRepository.CreateHoliday(newHoliday, cancellationToken);
    }
}

public record AddHolidayCommand(HolidayDTO HolidayDTO) : IRequest;
