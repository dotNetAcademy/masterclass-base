using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Domain.Models
{
    public class Employee
    {
        public Employee(string id, string firstName, string lastName, string email, string role) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public string Id { get; private set; } // TODO adapt to Auth0 response
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }

        private readonly ICollection<Timesheet> _timesheets = new List<Timesheet>();
        public IReadOnlyCollection<Timesheet> Timesheets => _timesheets.ToList();

        public void InitTimesheets(List<Timesheet> timesheets)
        {
            _timesheets.Clear();
            timesheets.ForEach(_timesheets.Add);
        }

        public void AddTimesheet(Timesheet timesheet)
        {
            _timesheets.Add(timesheet);
        }

        public void AddRegistration(Registration registration)
        {
            var timesheet = _timesheets.SingleOrDefault(t => t.Year == registration.TimeSlot.Start.Year && t.Month == registration.TimeSlot.Start.Month);
            if (timesheet is null)
            {
                timesheet = new Timesheet(registration.TimeSlot.Start.Year, registration.TimeSlot.Start.Month);
                AddTimesheet(timesheet);
            }
            timesheet.AddRegistration(registration);
        }

    }
}
