using AutoFixture;
using NSubstitute;
using Shouldly;
using TimesheetApp.Domain.Interfaces;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.StaticClasses;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.UnitTests
{
    public class RegistrationTests
    {
        private Fixture _fixture;
        private IEmployeeRepository _employeeRepository;
        
        public RegistrationTests()
        {
            Random rnd = new Random();
            _fixture = new Fixture();

            _fixture.Customize<Employee>(e => e.FromFactory(() => new Employee(
                _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>()
            )));
            _fixture.Customize<Timesheet>(t => t.FromFactory(() => new Timesheet(
                2023, rnd.Next(1,13)
            )));
            _fixture.Customize<Registration>(r => r.FromFactory(() => new Registration(
                RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 1, 9, 0, 0), new DateTime(2023, 4, 1, 17, 30, 0))
            )));

            _employeeRepository = Substitute.For<IEmployeeRepository>();
        }

        [Fact]
        public void AddRegistration_EmployeeExistsAndTimeSheetExists_RegistrationAdded()
        {
            // Arrange
            var registrations = _fixture.Build<Registration>().CreateMany().ToList();
            var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
            registrations.ForEach(r =>
            {
                timesheets.ForEach(t => t.AddRegistration(r));
            });
            var employee = _fixture.Build<Employee>().Create();
            employee.InitTimesheets(timesheets);

            var registration = new Registration(
                RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 1, 9, 0, 0), new DateTime(2023, 4, 1, 17, 30, 0))
            );
            var timesheet = new Timesheet(registration.TimeSlot.Start.Year, registration.TimeSlot.Start.Month);
            employee.AddTimesheet(timesheet);

            var timesheetCount = employee.Timesheets.Count;
            var registrationCount = employee.Timesheets.Sum(t => t.Registrations.Count);

            // Act
            employee.AddRegistration(registration);

            // Assert
            employee.Timesheets.Count.ShouldBe(timesheetCount);
            employee.Timesheets.Sum(t => t.Registrations.Count).ShouldBe(registrationCount+1);
        }

        [Fact]
        public void AddRegistration_EmployeeExistsAndTimeSheetDoesntExist_RegistrationAndTimesheetAdded()
        {
            // Arrange
            var registrations = _fixture.Build<Registration>().CreateMany().ToList();
            var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
            registrations.ForEach(r =>
            {
                timesheets.ForEach(t => t.AddRegistration(r));
            });
            var employee = _fixture.Build<Employee>().Create();
            employee.InitTimesheets(timesheets);

            var registration = new Registration(
                RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 1, 9, 0, 0), new DateTime(2023, 4, 1, 17, 30, 0))
            );

            var timesheetCount = employee.Timesheets.Count;
            var registrationCount = employee.Timesheets.Sum(t => t.Registrations.Count);

            // Act
            employee.AddRegistration(registration);

            // Assert
            employee.Timesheets.Count.ShouldBe(timesheetCount+1);
            employee.Timesheets.Sum(t => t.Registrations.Count).ShouldBe(registrationCount + 1);
        }
    }
}