using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public class HolidayMapper
{
    public static HolidayDTO ToDto(Holiday holiday)
    {
        return new HolidayDTO
        {
            Date = holiday.Date,
            Name = holiday.Name,
        };
    }
}
