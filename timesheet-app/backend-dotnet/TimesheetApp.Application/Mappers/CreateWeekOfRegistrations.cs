using System.Globalization;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public class CreateWeekOfRegistrations
{
    public static WeekOfRegistrationsDTO MakeWeekOfRegistrations(IEnumerable<Registration> registrations)
    {
        return new WeekOfRegistrationsDTO
        {
            WeekNumber = GetWeekNumber(registrations.First().TimeSlot.Start),
            AmountOfRegistrations = registrations.Count(),
            WorkDays = GetAmountOfDayType("workday", registrations),
            SickDays = GetAmountOfDayType("sickday", registrations),
            VacationDays = GetAmountOfDayType("vacationday", registrations),
            TotalHours = registrations.Sum(r => r.TimeSlot.TotalHours),
            Registrations = registrations.Select(r => ConvertToRegistrationDTO.MapRegistrationToDTO(r, false)).ToList()
        };
    }

    private static int GetWeekNumber(DateTime date)
    {
        return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
    }

    private static int GetAmountOfDayType(string type, IEnumerable<Registration> registrations)
    {
        return registrations.Where(r => r.RegistrationType.ToLower() == type).Count();
    }
}
