using TimesheetApp.Application.Interfaces.Validators;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.Validators;

public class RegistrationValidator : IRegistrationValidator
{
    public RegistrationValidator() { }
    public bool CheckIfRegistrationOverlapsWithHoliday(DateTime date, List<Holiday> holidays)
    {
        date = date.AddHours(-date.Hour);
        date = date.AddMinutes(-date.Minute);

        if (holidays.Where(h => h.Date == date).ToList().Count > 0)
        {
            return true;
        }
        return false;
    }
}
