using System.Data;
using Microsoft.EntityFrameworkCore;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Interfaces.Validators;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Validators;

namespace TimesheetApp.Infrastructure.Repositories;

public class HolidayRepository : IHolidayRepository
{
    private readonly AppDbContext _context;

    public HolidayRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Holiday>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Holidays.OrderBy(h => h.Date).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Holiday>> GetByDate(DateTime date, CancellationToken cancellationToken)
    {
        return await _context.Holidays.Where(h => h.Date == date).ToListAsync(cancellationToken);
    }

    public async Task CreateHoliday(Holiday holiday, CancellationToken cancellationToken)
    {
        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
