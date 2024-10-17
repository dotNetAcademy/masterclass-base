using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Interfaces.Repositories;

public interface ITimesheetRepository
{
    Task<IEnumerable<Timesheet>> GetByUserId(string userId, CancellationToken cancellationToken);

    Task<Timesheet?> GetById(int id, CancellationToken cancellationToken);

    Task<Timesheet> CheckIfTimesheetOfRegistrationsIsApproved(int month, int year, string employeeId, CancellationToken cancellationToken);

    Task Update(Timesheet timesheet, CancellationToken cancellationToken);
}
