namespace TimesheetApp.Application.DTOs;

public class RegistrationDTO
{
    public int Id { get; set; }
    public required string RegistrationType { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool IsTimesheetApproved { get; set; }
    public string? Auth0Id { get; set; }
    public double TotalHours { get; set; }
}
