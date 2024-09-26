namespace TimesheetApp.Application.DTOs;

public class AddRegistrationDTO
{
    public required string RegistrationType { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public required string Auth0Id { get; set; }
}
