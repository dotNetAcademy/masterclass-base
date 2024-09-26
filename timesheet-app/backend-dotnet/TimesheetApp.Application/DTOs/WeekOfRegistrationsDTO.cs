namespace TimesheetApp.Application.DTOs;

public class WeekOfRegistrationsDTO
{
    public int WeekNumber { get; set; }
    public int AmountOfRegistrations { get; set; }
    public int WorkDays { get; set; }
    public int SickDays { get; set; }
    public int VacationDays { get; set; }
    public double TotalHours { get; set; }
    public List<RegistrationDTO>? Registrations { get; set; }
}
