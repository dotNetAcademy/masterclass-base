using System.Data;
using Microsoft.EntityFrameworkCore;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Repositories;

public class TimesheetRepository : ITimesheetRepository
{
    private readonly AppDbContext _context;

    public TimesheetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Timesheet>> GetByUserId(string employeeId)
    {
        var timesheets = await _context.Employees.Where(e => e.Id == employeeId)
            .SelectMany(e => e.Timesheets)
            .Include(t => t.Registrations)
            .ToListAsync();
        return timesheets;
    }

    private static Timesheet? CheckIfTimesheetAlreadyInList(Timesheet timesheet, List<Timesheet> timesheets)
    {
        return timesheets.SingleOrDefault(t => t.Id == timesheet.Id);
    }

    public async Task Update(Timesheet timesheet)
    {
        _context.Timesheets.Update(timesheet);
        await _context.SaveEntitiesAsync();
    }

    public async Task<Timesheet?> GetById(int id)
    {
        var timesheet = await _context.Timesheets.Where(t => t.Id == id).SingleOrDefaultAsync();
        return timesheet;
    }

    public async Task<Timesheet> CheckIfTimesheetOfRegistrationsIsApproved(int month, int year, string employeeId)
    {
        var timesheet = await _context.Employees.Where(e => e.Id == employeeId)
            .Select(e => e.Timesheets.Where(t => t.Month == month && t.Year == year).First())
            .SingleAsync();
        return timesheet;
    }
}
