using System.Data;
using Microsoft.EntityFrameworkCore;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Repositories;

public class TimesheetRepository : ITimesheetRepository
{
    private readonly AppDbContext _context;

    public TimesheetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Timesheet>> GetByUserId(string employeeId, CancellationToken cancellationToken)
    {
        return await _context.Employees.Where(e => e.Id == employeeId)
            .SelectMany(e => e.Timesheets)
            .Include(t => t.Registrations)
            .ToListAsync(cancellationToken);
    }

    private static Timesheet? CheckIfTimesheetAlreadyInList(Timesheet timesheet, List<Timesheet> timesheets)
    {
        return timesheets.SingleOrDefault(t => t.Id == timesheet.Id);
    }

    public async Task Update(Timesheet timesheet, CancellationToken cancellationToken)
    {
        _context.Timesheets.Update(timesheet);
        await _context.SaveEntitiesAsync(cancellationToken);
    }

    public async Task<Timesheet?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _context.Timesheets.Where(t => t.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Timesheet> CheckIfTimesheetOfRegistrationsIsApproved(int month, int year, string employeeId, CancellationToken cancellationToken)
    {
        return await _context.Employees.Where(e => e.Id == employeeId)
            .Select(e => e.Timesheets.Where(t => t.Month == month && t.Year == year).First())
            .SingleAsync(cancellationToken);
    }
}
