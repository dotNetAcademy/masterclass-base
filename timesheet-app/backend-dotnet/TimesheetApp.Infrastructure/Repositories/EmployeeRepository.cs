using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PronostiekApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Domain.Interfaces;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string EMPLOYEE_COMPLETE_QUERY = @"
                    SELECT e.*, t.*, r.* 
                    FROM Employee e
                    LEFT JOIN Timesheet t on t.EmployeeId = e.Id
                    LEFT JOIN Registration r on r.TimesheetId = t.Id ";

        private readonly IDbConnection _connection;
        private readonly AppDbContext _context;

        public EmployeeRepository(IConfiguration configuration, AppDbContext context)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("defaultconnection"));
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            string sql = EMPLOYEE_COMPLETE_QUERY;

            var result = await DapperQuery(sql);
            return GroupEmployees(result);
        }

        public async Task<Employee?> GetById(string id)
        {
            string sql = EMPLOYEE_COMPLETE_QUERY + " WHERE e.Id = @id";

            var result = await DapperQuery(sql, id);
            return GroupEmployees(result).SingleOrDefault();
        }

        private async Task<IEnumerable<Employee>> DapperQuery(string sql, string? id = null)
        {
            var result = await _connection.QueryAsync<Employee, Timesheet, Registration, TimeSlot, Employee>(sql, (employee, timesheet, registration, timeSlot) =>
            {
                if (timesheet is not null)
                {
                    if (registration is not null)
                    {
                        registration.ChangeTimeSlot(timeSlot);
                        timesheet.AddRegistration(registration);
                    }
                    employee.AddTimesheet(timesheet);
                }

                return employee;
            }, new { id }, splitOn: "Id, Id, Id, Start");

            return result;
        }

        private static IEnumerable<Employee> GroupEmployees(IEnumerable<Employee> result)
        {
            return result.GroupBy(e => e.Id).Select(e =>
            {
                Employee employee = e.First();
                if (e.First().Timesheets.Count > 0)
                {
                    var timesheets = e.SelectMany(e => e.Timesheets!).ToList()
                        .GroupBy(t => t.Id).Select(t =>
                        {
                            Timesheet timesheet = t.First();
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
    }
}
