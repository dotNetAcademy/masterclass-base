using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Interfaces.Validators;

public interface IHolidayValidator
{
    bool CheckIfThereIsAlreadyAHolidayRegistredOnDay(DateTime date, List<Holiday> holidays);
}
