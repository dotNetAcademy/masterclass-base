using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Interfaces.Validators;

public interface IRegistrationValidator
{
    bool CheckIfRegistrationOverlapsWithHoliday(DateTime date, List<Holiday> holidays);
}
