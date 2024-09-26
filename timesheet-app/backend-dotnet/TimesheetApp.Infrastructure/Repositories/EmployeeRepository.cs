using System.Data;
using Microsoft.EntityFrameworkCore;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        var result = await _context.Employees.Include(e => e.Timesheets).ThenInclude(t => t.Registrations).ToListAsync();

        return GroupEmployees(result);
    }

    public async Task<Employee?> GetById(string id)
    {
        var result = await _context.Employees.Where(e => e.Id == id)
            .Include(e => e.Timesheets)
            .ThenInclude(t => t.Registrations)
            .ToListAsync();


        return GroupEmployees(result).SingleOrDefault();
    }

    private static IEnumerable<Employee> GroupEmployees(IEnumerable<Employee> result)
    {
        return result.GroupBy(e => e.Id).Select(e =>
        {
            var employee = e.First();
            if (e.First().Timesheets.Count > 0)
            {
                var timesheets = e.SelectMany(e => e.Timesheets!).ToList()
                    .GroupBy(t => t.Id).Select(t =>
                    {
                        var timesheet = t.First();
                        if (t.First().Registrations is not null)
                        {
                            var registrations = t.SelectMany(t => t.Registrations).ToList();
                            timesheet.InitRegistrations(registrations);
                        }
                        return timesheet;
                    }).ToList();

                employee.InitTimesheets(timesheets);
            }
            return employee;
        });
    }

    public async Task Update(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<Employee?> GetByAuth0Id(string auth0Id)
    {
        var employee = await _context.Employees.Where(e => e.Auth0Id == auth0Id).FirstOrDefaultAsync();

        return employee;
    }

    public async Task<IEnumerable<Employee>> GetByName(string name)
    {
        var result = await _context.Employees.Where(e => EF.Functions.Like($"{e.FirstName} {e.LastName}", name))
            .Include(e => e.Timesheets)
            .ThenInclude(t => t.Registrations)
            .ToListAsync();

        return GroupEmployees(result);
    }
}
