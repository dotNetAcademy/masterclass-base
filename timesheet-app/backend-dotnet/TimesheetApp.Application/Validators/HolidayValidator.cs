using TimesheetApp.Application.Interfaces.Validators;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.Validators;

public class HolidayValidator : IHolidayValidator
{
    public HolidayValidator()
    {
    }
    public bool CheckIfThereIsAlreadyAHolidayRegistredOnDay(DateTime date, List<Holiday> holidays)
    {
        if (holidays.Where(h => h.Date == date).ToList().Count > 0)
        {
            return true;
        }
        return false;
    }
}
