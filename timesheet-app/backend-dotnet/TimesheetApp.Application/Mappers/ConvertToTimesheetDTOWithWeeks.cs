using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public class ConvertToTimesheetDTOWithWeeks
{
    public static TimesheetDTO MapToTimesheetDTOWithWeeks(Timesheet timesheet)
    {
        DateTime startDate, endDate, dateToCheckFrom;
        dateToCheckFrom = new DateTime(timesheet.Year, timesheet.Month, 1);
        (startDate, endDate) = GetFirstAndLastDayOfWeek(dateToCheckFrom);

        var weeks = new List<WeekOfRegistrationsDTO>();

        while (dateToCheckFrom.Month == timesheet.Month)
        {
            (startDate, endDate) = GetFirstAndLastDayOfWeek(dateToCheckFrom);
            var registrationsInWeek = timesheet.Registrations.Where(r => r.TimeSlot.Start >= startDate && r.TimeSlot.Start <= endDate);
            if (registrationsInWeek.Count() > 0)
            {
                weeks.Add(CreateWeekOfRegistrations.MakeWeekOfRegistrations(registrationsInWeek.ToList()));
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

    private static (DateTime, DateTime) GetFirstAndLastDayOfWeek(DateTime date)
    {
        var diffrence = date.DayOfWeek - DayOfWeek.Monday;

        if (diffrence < 0)
        {
            diffrence += 7;
        }

        var startDate = date.AddDays(-diffrence).Date;

        return (startDate, startDate.AddDays(6));
    }
}
