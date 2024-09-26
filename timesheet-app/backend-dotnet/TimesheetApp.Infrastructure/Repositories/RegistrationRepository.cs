using Microsoft.EntityFrameworkCore;
using System.Data;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly AppDbContext _context;

    public RegistrationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Registration>> GetAllAsync()
    {
        var registrations = await _context.Registrations.ToListAsync();

        return registrations;
    }

    public async Task<IEnumerable<Registration>> GetByDateAsync(DateTime date)
    {
        var registrations = await _context.Registrations.Where(r => r.TimeSlot.Start.Date == date.Date).ToListAsync();

        return registrations;
    }

    public async Task<Registration?> GetByIdAsync(long id)
    {
        var registration = await _context.Registrations.Where(r => r.Id == id).SingleOrDefaultAsync();

        return registration;
    }

    public async Task<IEnumerable<Registration>> GetByDateAndEmployeeAsync(DateTime date, string employeeId)
    {
        return await _context.Employees
            .Where(e => e.Id == employeeId)
            .SelectMany(e => e.Timesheets)
            .SelectMany(t => t.Registrations)
            .Where(r => r.TimeSlot.Start.Date == date.Date)
            .ToListAsync();
    }

    public async Task Update(Registration registration)
    {
        _context.Registrations.Update(registration);
        await _context.SaveChangesAsync();
    }
}
