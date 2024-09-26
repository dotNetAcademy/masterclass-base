using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Interfaces.Services;

namespace TimesheetApp.Domain.Models;

public class Holiday
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; }

    public Holiday(DateTime date, string name, IValidateHoliday validateHoliday)
    {
        var hasAlreadyHolidayOnDate = validateHoliday.CheckIfThereIsAlreadyAHolidayRegistredOnDay(date).Result;
        if (!hasAlreadyHolidayOnDate)
        {
            Date = date;
            Name = name;
        }
        else
        {
            throw new AppException("There is already a holiday registered on this day");
        }

    }
}
