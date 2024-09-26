namespace TimesheetApp.Application.DTOs;

public class TimesheetDTO
{
    public int Id { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public List<WeekOfRegistrationsDTO>? WeekOfRegistrations { get; set; }
    public double TotalHours { get; set; }
    public bool IsSubmitted { get; set; }
    public bool IsApproved { get; set; }
    public bool CanBeSubmitted { get; set; }
}
