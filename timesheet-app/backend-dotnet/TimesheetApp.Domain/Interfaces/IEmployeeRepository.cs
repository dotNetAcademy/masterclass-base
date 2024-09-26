using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee?> GetById(string id);
    Task Update(Employee employee);
}
