namespace TimesheetApp.Domain.Interfaces.Services;

public interface IValidateHoliday
{
    Task<bool> CheckIfThereIsAlreadyAHolidayRegistredOnDay(DateTime date);
}
