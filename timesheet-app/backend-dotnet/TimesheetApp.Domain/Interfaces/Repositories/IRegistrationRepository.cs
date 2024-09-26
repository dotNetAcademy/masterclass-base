using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.Interfaces.Repositories;

public interface IRegistrationRepository
{
    Task<IEnumerable<Registration>> GetAllAsync();

    Task<IEnumerable<Registration>> GetByDateAsync(DateTime date);

    Task<IEnumerable<Registration>> GetByDateAndEmployeeAsync(DateTime date, string id);

    Task Update(Registration registration);

    Task<Registration?> GetByIdAsync(long id);
}
