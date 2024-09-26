using System.Data;
using Microsoft.EntityFrameworkCore;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Interfaces.Services;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Services;

namespace TimesheetApp.Infrastructure.Repositories;

public class HolidayRepository : IHolidayRepository
{
    private readonly AppDbContext _context;

    public HolidayRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Holiday>> GetAll()
    {
        var holidays = await _context.Holidays.OrderBy(h => h.Date).ToListAsync();
        return holidays;
    }

    public async Task<IEnumerable<Holiday>> GetByDate(DateTime date)
    {
        var holidays = await _context.Holidays.Where(h => h.Date == date).ToListAsync();
        return holidays;
    }

    public async Task CreateHoliday(DateTime date, string name)
    {
        IValidateHoliday validateHoliday = new ValidateHoliday(this);
        var holiday = new Holiday(date, name, validateHoliday);
        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync();
    }
}
