using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Models.Enums;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Domain.Models;

public class Timesheet
{
    public Timesheet()
    { }

    public Timesheet(int year, int month)
    {
        Year = year;
        Month = month;
        _registrations = new List<Registration>();
    }

    public int Id { get; private set; }
    public int Year { get; private set; }
    public int Month { get; private set; }

    public bool IsSubmitted { get; private set; }
    public bool IsApproved { get; private set; }

    public readonly ICollection<Registration> _registrations = new List<Registration>();
    public IReadOnlyCollection<Registration> Registrations => _registrations.ToList();

    public void InitRegistrations(List<Registration> registrations)
    {
        _registrations.Clear();
        registrations.ForEach(_registrations.Add);
    }

    public void AddRegistration(Registration registration)
    {
        if (IsApproved)
        {
            throw new AppException("The timesheet is already approved, no changes are allowed");
        }
        RegistrationExceedsDayLimitOf8Hours(registration.TimeSlot, registration.Id);
        RegistrationExceedsWeekLimitOf40Hours(registration.TimeSlot, registration.Id);

        _registrations.Add(registration);
    }

    public void AddRegistrationsToCreateObject(Registration registration)
    {
        _registrations.Add(registration);
    }

    public void UpdateRegistration(int registrationId, RegistrationType registrationType, TimeSlot timeSlot)
    {
        if (IsApproved)
        {
            throw new AppException("The timesheet is already approved, no changes are allowed");
        }
        RegistrationExceedsDayLimitOf8Hours(timeSlot, registrationId);
        RegistrationExceedsWeekLimitOf40Hours(timeSlot, registrationId);
        var registrationToUpdate = Registrations.Single(r => r.Id == registrationId);
        registrationToUpdate.UpdateRegistration(registrationType, timeSlot);
    }

    public void SubmitTimesheet()
    {
        if (CanBeSubmitted())
        {
            IsSubmitted = true;
        }
        else
        {
            throw new AppException("Not all requirements are met to submit the timesheet");
        }
    }

    public void ApproveTimesheet()
    {

        if (IsSubmitted)
        {
            IsApproved = true;
        }
        else
        {
            throw new AppException("The timesheet needs to be submitted before you can approve it");
        }
    }

    public void RegistrationExceedsWeekLimitOf40Hours(TimeSlot timeSlot, int? registrationId)
    {
        var registrationsFromSameWeek = GetRegistrationsFromSameWeek(timeSlot.Start);

        var totalHoursOfWeek = timeSlot.TotalHours;
        foreach (var reg in registrationsFromSameWeek.Where(reg => reg.Id != registrationId))
        {
            totalHoursOfWeek += reg.TimeSlot.TotalHours;
        }
        if (totalHoursOfWeek > 40)
        {
            throw new AppException("You exceeded the maximum of 40 hours for this week");
        }
    }

    public IEnumerable<Registration> GetRegistrationsFromSameWeek(DateTime date)
    {
        DateTime startDate, endDate;
        (startDate, endDate) = GetFirstAndLastDayFromWeek(date);
        return Registrations.Where(r => r.TimeSlot.Start.Day >= startDate.Day && r.TimeSlot.Start.Day <= endDate.Day);
    }

    public static (DateTime, DateTime) GetFirstAndLastDayFromWeek(DateTime date)
    {
        var diffrence = date.DayOfWeek - DayOfWeek.Monday;

        if (diffrence < 0)
        {
            diffrence += 7;
        }

        var startDate = date.AddDays(-diffrence).Date;

        return (startDate, startDate.AddDays(6));
    }

    public void RegistrationExceedsDayLimitOf8Hours(TimeSlot timeSlot, int? registrationId)
    {
        var registrationsFromSameDay = Registrations.Where(r => r.TimeSlot.Start.Day == timeSlot.Start.Day &&
        r.Id != registrationId);
        var totalHoursOfDay = timeSlot.TotalHours;
        foreach (var reg in registrationsFromSameDay)
        {
            totalHoursOfDay += reg.TimeSlot.TotalHours;
            timeSlot.IsOverlapping(reg.TimeSlot);
        }
        if (totalHoursOfDay > 8)
        {
            throw new AppException("You exceeded the maximum of 8 hours for this day");
        }
    }

    public bool CanBeSubmitted()
    {
        if (AllWorkingDaysHaveARegistration() && AverageOfMinimum8HoursADay())
        {
            return true;
        }
        return false;
    }

    private bool AllWorkingDaysHaveARegistration()
    {
        var amountOfWorkingDays = GetAmountOfWorkingDaysInTimesheet();
        var daysWithRegistrations = new List<int>();
        foreach (var registration in Registrations)
        {
            if (!daysWithRegistrations.Contains(registration.TimeSlot.Start.Day))
            {
                daysWithRegistrations.Add(registration.TimeSlot.Start.Day);
            }
        }

        if (daysWithRegistrations.Count == amountOfWorkingDays)
        {
            return true;
        }
        return false;
    }

    private int GetAmountOfWorkingDaysInTimesheet()
    {
        var amountOfWorkingDays = 0;
        var firstDay = new DateTime(Year, Month, 1);
        var lastDay = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
        for (var i = firstDay; i <= lastDay; i = i.AddDays(1))
        {
            if (i.DayOfWeek is not DayOfWeek.Saturday and not DayOfWeek.Sunday)
            {
                amountOfWorkingDays++;
            }
        }

        return amountOfWorkingDays;
    }

    private bool AverageOfMinimum8HoursADay()
    {
        var totalHoursInTimesheet = Registrations.Sum(r => r.TimeSlot.TotalHours);
        var amountOfWorkingDays = GetAmountOfWorkingDaysInTimesheet();
        if (totalHoursInTimesheet / amountOfWorkingDays >= 8)
        {
            return true;
        }
        return false;
    }
}
