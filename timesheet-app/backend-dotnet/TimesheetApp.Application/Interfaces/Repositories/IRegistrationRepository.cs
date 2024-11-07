using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Interfaces.Repositories;

public interface IRegistrationRepository
{
    Task<IEnumerable<Registration>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Registration>> GetByDateAsync(DateTime date, CancellationToken cancellationToken);

    Task<IEnumerable<Registration>> GetByDateAndEmployeeAsync(DateTime date, string id, CancellationToken cancellationToken);

    Task Update(Registration registration, CancellationToken cancellationToken);

    Task<Registration?> GetByIdAsync(long id, CancellationToken cancellationToken);
}
