using System.Data;
using Microsoft.EntityFrameworkCore;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly AppDbContext _context;

    public RegistrationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Registration>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Registrations.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Registration>> GetByDateAsync(DateTime date, CancellationToken cancellationToken)
    {
        return await _context.Registrations.Where(r => r.TimeSlot.Start.Date == date.Date).ToListAsync(cancellationToken);
    }

    public async Task<Registration?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Registrations.Where(r => r.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Registration>> GetByDateAndEmployeeAsync(DateTime date, string employeeId, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Where(e => e.Id == employeeId)
            .SelectMany(e => e.Timesheets)
            .SelectMany(t => t.Registrations)
            .Where(r => r.TimeSlot.Start.Date == date.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task Update(Registration registration, CancellationToken cancellationToken)
    {
        _context.Registrations.Update(registration);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
