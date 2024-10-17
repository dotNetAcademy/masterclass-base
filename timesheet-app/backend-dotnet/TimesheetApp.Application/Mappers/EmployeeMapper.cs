using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public static class EmployeeMapper
{
    public static EmployeeDTO ToDto(this Employee employee)
    {
        return new EmployeeDTO
        {
            Name = $"{employee.FirstName} {employee.LastName}",
            Timesheets = employee.Timesheets.ToDto(),
        };
    }
}
