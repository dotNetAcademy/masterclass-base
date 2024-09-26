using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.Interfaces.Repositories;

public interface IHolidayRepository
{
    Task<IEnumerable<Holiday>> GetAll();
    Task<IEnumerable<Holiday>> GetByDate(DateTime date);
    Task CreateHoliday(DateTime date, string name);
}
