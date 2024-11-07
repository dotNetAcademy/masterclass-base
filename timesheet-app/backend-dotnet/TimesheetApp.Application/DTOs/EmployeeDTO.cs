namespace TimesheetApp.Application.DTOs;

public class EmployeeDTO
{
    public required string Name { get; set; }
    public IEnumerable<TimesheetDTO>? Timesheets { get; set; }
}
