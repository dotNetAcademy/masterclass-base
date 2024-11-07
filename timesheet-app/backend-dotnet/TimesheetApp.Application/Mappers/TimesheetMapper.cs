using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public static class TimesheetMapper
{
    public static List<TimesheetDTO> ToDto(this IEnumerable<Timesheet> timesheets)
    {
        return timesheets.Select(timesheet => timesheet.ToDto()).ToList();
    }

    public static TimesheetDTO ToDto(this Timesheet timesheet)
    {
        var dateToCheckFrom = new DateTime(timesheet.Year, timesheet.Month, 1);
        var startDate = dateToCheckFrom.GetFirstDayOfWeek();
        var endDate = startDate.GetLastDayOfWeek();

        var weeks = new List<WeekOfRegistrationsDTO>();

        while (dateToCheckFrom.Month == timesheet.Month)
        {
            startDate = dateToCheckFrom.GetFirstDayOfWeek();
            endDate = startDate.GetLastDayOfWeek();
            var registrationsInWeek = timesheet.Registrations.Where(r => r.TimeSlot.Start >= startDate && r.TimeSlot.Start <= endDate);
            if (registrationsInWeek.Count() > 0)
            {
                weeks.Add(WeekOfRegistrationMapper.ToDto(registrationsInWeek.ToList()));
            }
            dateToCheckFrom = dateToCheckFrom.AddDays(6);
        }

        return new TimesheetDTO
        {
            Id = timesheet.Id,
            Month = timesheet.Month,
            Year = timesheet.Year,
            WeekOfRegistrations = weeks,
            TotalHours = weeks.Sum(w => w.TotalHours),
            IsSubmitted = timesheet.IsSubmitted,
            IsApproved = timesheet.IsApproved,
            CanBeSubmitted = timesheet.CanBeSubmitted()
        };
    }

    private static DateTime GetFirstDayOfWeek(this DateTime date)
    {
        var diffrence = date.DayOfWeek - DayOfWeek.Monday;

        if (diffrence < 0)
        {
            diffrence += 7;
        }

        return date.AddDays(-diffrence).Date;
    }

    private static DateTime GetLastDayOfWeek(this DateTime startDate)
    {
        return startDate.AddDays(6).Date;
    }
}
