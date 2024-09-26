using TimesheetApp.Domain.Interfaces.Services;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Domain.Models;

public class Employee
{
    public Employee(string id, string firstName, string lastName, string email, string role, string auth0Id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = role;
        Auth0Id = auth0Id;
    }

    public string Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; }

    public string? Auth0Id { get; private set; }

    private readonly ICollection<Timesheet> _timesheets = new List<Timesheet>();
    public IReadOnlyCollection<Timesheet> Timesheets => _timesheets.ToList();

    public void InitTimesheets(List<Timesheet> timesheets)
    {
        _timesheets.Clear();
        timesheets.ForEach(_timesheets.Add);
    }

    public void AddTimesheet(Timesheet timesheet)
    {
        _timesheets.Add(timesheet);
    }

    public async Task AddRegistration(Registration registration, IValidateRegistration validateRegistration)
    {
        var timesheet = _timesheets.SingleOrDefault(t => t.Year == registration.TimeSlot.Start.Year && t.Month == registration.TimeSlot.Start.Month);

        if (timesheet is null)
        {
            timesheet = new Timesheet(registration.TimeSlot.Start.Year, registration.TimeSlot.Start.Month);
            AddTimesheet(timesheet);
        }


        await timesheet.AddRegistration(registration, validateRegistration);
    }

    public void UpdateRegistration(int registrationId, string registrationType, TimeSlot timeSlot)
    {
        var timesheet = _timesheets.Single(t => t.Year == timeSlot.Start.Year && t.Month == timeSlot.Start.Month);
        timesheet.UpdateRegistration(registrationId, registrationType, timeSlot);
    }
}
