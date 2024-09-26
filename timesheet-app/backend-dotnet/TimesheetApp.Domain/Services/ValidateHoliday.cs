using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Interfaces.Services;

namespace TimesheetApp.Domain.Services;

public class ValidateHoliday : IValidateHoliday
{
    private readonly IHolidayRepository _holidayRepository;
    public ValidateHoliday(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }
    public async Task<bool> CheckIfThereIsAlreadyAHolidayRegistredOnDay(DateTime date)
    {
        var holidays = await _holidayRepository.GetByDate(date);
        if (holidays.ToList().Count > 0)
        {
            return true;
        }
        return false;
    }
}
