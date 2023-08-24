using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Domain.Models
{
    public class Timesheet
    {
        public Timesheet() { }
        public Timesheet(int year, int month)
        {
            Year = year;
            Month = month;
            _registrations = new List<Registration>();
        }

        public int Id { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }

        public readonly ICollection<Registration> _registrations = new List<Registration>();
        public IReadOnlyCollection<Registration> Registrations => _registrations.ToList();

        public void InitRegistrations(List<Registration> registrations)
        {
            _registrations.Clear();
            registrations.ForEach(_registrations.Add);
        }

        public void AddRegistration(Registration registration)
        {
            _registrations.Add(registration);
        }
    }
}
