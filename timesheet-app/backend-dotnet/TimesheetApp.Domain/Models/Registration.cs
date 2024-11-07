using TimesheetApp.Domain.Models.Enums;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Domain.Models;

public class Registration
{
    public Registration(RegistrationType registrationType, TimeSlot timeSlot)
    {
        RegistrationType = registrationType;
        TimeSlot = timeSlot;
    }

    private Registration()
    {
    }

    public int Id { get; internal set; }
    public RegistrationType RegistrationType { get; private set; }
    public TimeSlot TimeSlot { get; private set; } = default!;

    public void ChangeTimeSlot(TimeSlot timeSlot)
    {
        TimeSlot = timeSlot;
    }

    public void UpdateRegistration(RegistrationType registrationType, TimeSlot timeSlot)
    {
        RegistrationType = registrationType;
        ChangeTimeSlot(timeSlot);
    }
}
