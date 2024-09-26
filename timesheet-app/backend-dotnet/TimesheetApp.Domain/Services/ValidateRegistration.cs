using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Interfaces.Services;

namespace TimesheetApp.Domain.Services;

public class ValidateRegistration : IValidateRegistration
{
    private readonly IHolidayRepository _holidayRepository;

    public ValidateRegistration(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }
    public async Task<bool> CheckIfRegistrationOverlapsWithHoliday(DateTime date)
    {
        date = date.AddHours(-date.Hour);
        date = date.AddMinutes(-date.Minute);

        var holidays = await _holidayRepository.GetByDate(date);
        if (holidays.ToList().Count > 0)
        {
            return true;
        }
        return false;
    }
}
