using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Interfaces.Repositories;

public interface IHolidayRepository
{
    Task<IEnumerable<Holiday>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<Holiday>> GetByDate(DateTime date, CancellationToken cancellationToken);
    Task CreateHoliday(Holiday holiday, CancellationToken cancellationToken);
}
