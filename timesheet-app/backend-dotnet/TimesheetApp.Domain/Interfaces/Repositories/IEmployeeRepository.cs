using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee?> GetById(string id);
    Task<Employee?> GetByAuth0Id(string auth0Id);
    Task<IEnumerable<Employee>> GetByName(string name);
    Task Update(Employee employee);
}
