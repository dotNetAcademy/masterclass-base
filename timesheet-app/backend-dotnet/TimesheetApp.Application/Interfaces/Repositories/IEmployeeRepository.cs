using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken);
    Task<Employee?> GetById(string id, CancellationToken cancellationToken);
    Task<Employee?> GetByAuth0Id(string auth0Id, CancellationToken cancellationToken);
    Task<IEnumerable<Employee>> GetByName(string name, CancellationToken cancellationToken);
    Task Update(Employee employee, CancellationToken cancellationToken);
}
