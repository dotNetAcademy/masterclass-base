namespace TimesheetApp.Domain.Interfaces.Services;

public interface IValidateRegistration
{
    Task<bool> CheckIfRegistrationOverlapsWithHoliday(DateTime date);
}
