using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public class ConvertToHolidayDTO
{
    public static HolidayDTO MapToHolidayDTO(Holiday holiday)
    {
        return new HolidayDTO
        {
            Date = holiday.Date,
            Name = holiday.Name,
        };
    }
}
