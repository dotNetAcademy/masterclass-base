using MediatR;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Commands.HolidayCommands.AddHoliday;

public class AddHolidayHandler : IRequestHandler<AddHolidayCommand>
{
    private readonly IHolidayRepository _holidayRepository;

    public AddHolidayHandler(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }

    public async Task Handle(AddHolidayCommand request, CancellationToken cancellationToken)
    {
        await _holidayRepository.CreateHoliday(request.HolidayDTO.Date, request.HolidayDTO.Name);
    }
}
