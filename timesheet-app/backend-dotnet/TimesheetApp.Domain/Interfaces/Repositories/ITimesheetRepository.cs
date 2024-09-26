using TimesheetApp.Domain.Models;

namespace TimesheetApp.Domain.Interfaces.Repositories;

public interface ITimesheetRepository
{
    Task<IEnumerable<Timesheet>> GetByUserId(string userId);

    Task<Timesheet?> GetById(int id);

    Task<Timesheet> CheckIfTimesheetOfRegistrationsIsApproved(int month, int year, string employeeId);

    Task Update(Timesheet timesheet);
}
