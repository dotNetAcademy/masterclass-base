using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public class ConvertToEmployeeDTOWithTimesheets
{
    public static EmployeeDTO MapToEmployeeDTOWithTimesheets(Employee employee)
    {
        var employeeDTO = new EmployeeDTO
        {
            Name = $"{employee.FirstName} {employee.LastName}",
            Timesheets = new List<TimesheetDTO>()
        };

        var timesheetsWithRegistrations =
            from timesheet in employee.Timesheets
            where timesheet.Registrations.Count > 0
            select timesheet;

        var timesheets = timesheetsWithRegistrations.Select(ConvertToTimesheetDTOWithWeeks.MapToTimesheetDTOWithWeeks);

        employeeDTO.Timesheets = timesheets;

        return employeeDTO;
    }
}
