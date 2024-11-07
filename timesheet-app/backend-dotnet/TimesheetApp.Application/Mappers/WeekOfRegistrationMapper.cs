using System.Globalization;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Application.Mappers;

public class WeekOfRegistrationMapper
{
    public static WeekOfRegistrationsDTO ToDto(IEnumerable<Registration> registrations)
    {
        return new WeekOfRegistrationsDTO
        {
            WeekNumber = GetWeekNumber(registrations.First().TimeSlot.Start),
            AmountOfRegistrations = registrations.Count(),
            WorkDays = GetAmountOfDayType(RegistrationType.Workday, registrations),
            SickDays = GetAmountOfDayType(RegistrationType.Sickday, registrations),
            VacationDays = GetAmountOfDayType(RegistrationType.Vacationday, registrations),
            TotalHours = registrations.Sum(r => r.TimeSlot.TotalHours),
            Registrations = registrations.Select(r => RegistrationMapper.ToDto(r, false)).ToList()
        };
    }

    private static int GetWeekNumber(DateTime date)
    {
        return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
    }

    private static int GetAmountOfDayType(RegistrationType type, IEnumerable<Registration> registrations)
    {
        return registrations.Where(r => r.RegistrationType == type).Count();
    }
}
