using AutoFixture;
using NSubstitute;
using Shouldly;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Interfaces.Validators;
using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.Enums;
using TimesheetApp.Domain.Models.ValueObjects;
using TimesheetApp.Domain.Validators;

namespace TimesheetApp.UnitTests;

public class RegistrationTests
{
    private readonly Fixture _fixture;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHolidayRepository _holidayRepository;

    public RegistrationTests()
    {
        var rnd = new Random();
        _fixture = new Fixture();

        _fixture.Customize<Employee>(e => e.FromFactory(() => new Employee(
            _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<Role>(), _fixture.Create<string>()
        )));
        _fixture.Customize<Timesheet>(t => t.FromFactory(() => new Timesheet(
            2023, rnd.Next(1, 13)
        )));
        _fixture.Customize<Registration>(r => r.FromFactory(() => new Registration(
            RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 1, 9, 0, 0), new DateTime(2023, 4, 1, 17, 30, 0))
        )));

        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _holidayRepository = Substitute.For<IHolidayRepository>();
    }

    [Fact]
    public void AddRegistration_EmployeeExistsAndTimeSheetExists_RegistrationAdded()
    {
        // Arrange
        var registrations = _fixture.Build<Registration>().CreateMany().ToList();
        var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
        var today = DateTime.Now;
        var validateRegistration = new HolidayValidator();
        registrations.ForEach(r =>
        {
            r.ChangeTimeSlot(new TimeSlot(today, today));
            today = today.AddMinutes(1);
            timesheets.ForEach(t => t.AddRegistration(r));
        });
        var employee = _fixture.Build<Employee>().Create();
        employee.InitTimesheets(timesheets);

        var registration = new Registration(
            RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 4, 9, 0, 0), new DateTime(2023, 4, 4, 17, 00, 0))
        );
        var timesheet = new Timesheet(registration.TimeSlot.Start.Year, registration.TimeSlot.Start.Month);
        employee.AddTimesheet(timesheet);

        var timesheetCount = employee.Timesheets.Count;
        var registrationCount = employee.Timesheets.Sum(t => t.Registrations.Count);

        // Act
        employee.AddRegistration(registration);

        // Assert
        employee.Timesheets.Count.ShouldBe(timesheetCount);
        employee.Timesheets.Sum(t => t.Registrations.Count).ShouldBe(registrationCount + 1);
    }

    [Fact]
    public void AddRegistration_EmployeeExistsAndTimeSheetDoesntExist_RegistrationAndTimesheetAdded()
    {
        // Arrange
        var registrations = _fixture.Build<Registration>().CreateMany().ToList();
        var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
        var today = DateTime.Now;
        IRegistrationValidator validateRegistration = new RegistrationValidator();
        registrations.ForEach(r =>
        {
            r.ChangeTimeSlot(new TimeSlot(today, today));
            today = today.AddMinutes(1);
            timesheets.ForEach(t => t.AddRegistration(r));
        });
        var employee = _fixture.Build<Employee>().Create();
        employee.InitTimesheets(timesheets);

        var registration = new Registration(
            RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 4, 9, 0, 0), new DateTime(2023, 4, 4, 17, 00, 0))
        );

        var timesheetCount = employee.Timesheets.Count;
        var registrationCount = employee.Timesheets.Sum(t => t.Registrations.Count);

        // Act
        employee.AddRegistration(registration);

        // Assert
        employee.Timesheets.Count.ShouldBe(timesheetCount + 1);
        employee.Timesheets.Sum(t => t.Registrations.Count).ShouldBe(registrationCount + 1);
    }

    [Fact]
    public void AddRegistrationWithOverlappingTimeSlot_GiveErrorMessage_ThereIsAnOverlapWithAnotherRegistration()
    {
        // Arrange
        var registrations = _fixture.Build<Registration>().CreateMany().ToList();
        var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
        var today = DateTime.Now;
        IRegistrationValidator validateRegistration = new RegistrationValidator();
        registrations.ForEach(r =>
        {
            r.ChangeTimeSlot(new TimeSlot(today, today));
            today = today.AddMinutes(1);
            timesheets.ForEach(t => t.AddRegistration(r));
        });
        var employee = _fixture.Build<Employee>().Create();
        employee.InitTimesheets(timesheets);

        var reg1 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 6, 9, 0, 0), new DateTime(2023, 4, 6, 15, 30, 0)))
        {
            Id = 1
        };
        var overlap = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 6, 15, 25, 0), new DateTime(2023, 4, 6, 15, 30, 0)))
        {
            Id = 2
        };

        var timesheet = new Timesheet(reg1.TimeSlot.Start.Year, reg1.TimeSlot.Start.Month);
        employee.AddTimesheet(timesheet);

        employee.AddRegistration(reg1);
        var ex = Assert.Throws<AppException>(() => timesheet.AddRegistration(overlap));
        Assert.True(ex.Message == "There is an overlap with another registration");
    }

    [Fact]
    public void AddRegistrationWithMoreThan8Hours_GiveErrorMessage_ThisTimeslotExceedsTheMaximumOf8Hours()
    {
        // Arrange
        var registrations = _fixture.Build<Registration>().CreateMany().ToList();
        var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
        var today = DateTime.Now;
        IRegistrationValidator validateRegistration = new RegistrationValidator();
        registrations.ForEach(r =>
        {
            r.ChangeTimeSlot(new TimeSlot(today, today));
            today = today.AddMinutes(1);
            timesheets.ForEach(t => t.AddRegistration(r));
        });
        var employee = _fixture.Build<Employee>().Create();
        employee.InitTimesheets(timesheets);

        var ex = Assert.Throws<AppException>(() => new Registration(RegistrationType.Workday,
            new TimeSlot(new DateTime(2023, 4, 6, 9, 0, 0), new DateTime(2023, 4, 6, 18, 30, 0))));
        Assert.True(ex.Message == "This timeslot exceeds the maximum of 8 hours");
    }

    [Fact]
    public void AddRegistrationWichExceeds8HourLimitForDay_GiveErrorMessage_YouExceededTheMaximumOf8HoursForThisDay()
    {
        // Arrange
        var registrations = _fixture.Build<Registration>().CreateMany().ToList();
        var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
        var today = DateTime.Now;
        IRegistrationValidator validateRegistration = new RegistrationValidator();
        registrations.ForEach(r =>
        {
            r.ChangeTimeSlot(new TimeSlot(today, today));
            today = today.AddMinutes(1);
            timesheets.ForEach(t => t.AddRegistration(r));
        });
        var employee = _fixture.Build<Employee>().Create();
        employee.InitTimesheets(timesheets);

        var reg1 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 6, 9, 0, 0), new DateTime(2023, 4, 6, 15, 30, 0)))
        {
            Id = 1
        };
        var exceed8double = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 6, 15, 31, 0), new DateTime(2023, 4, 6, 20, 30, 0)))
        {
            Id = 3
        };

        var timesheet = new Timesheet(exceed8double.TimeSlot.Start.Year, exceed8double.TimeSlot.Start.Month);
        employee.AddTimesheet(timesheet);

        employee.AddRegistration(reg1);
        var ex = Assert.Throws<AppException>(() => timesheet.RegistrationExceedsDayLimitOf8Hours(exceed8double.TimeSlot, exceed8double.Id));
        Assert.True(ex.Message == "You exceeded the maximum of 8 hours for this day");
    }

    [Fact]
    public void AddRegistrationWichExceeds40HourLimitForWeek_GiveErrorMessage_YouExceededTheMaximumOf40HoursForThisWeek()
    {
        // Arrange
        var registrations = _fixture.Build<Registration>().CreateMany().ToList();
        var timesheets = _fixture.Build<Timesheet>().CreateMany().ToList();
        var today = DateTime.Now;
        IRegistrationValidator validateRegistration = new RegistrationValidator();
        registrations.ForEach(r =>
        {
            r.ChangeTimeSlot(new TimeSlot(today, today));
            today = today.AddMinutes(1);
            timesheets.ForEach(t => t.AddRegistration(r));
        });
        var employee = _fixture.Build<Employee>().Create();
        employee.InitTimesheets(timesheets);

        var reg1 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 6, 9, 0, 0), new DateTime(2023, 4, 6, 17, 30, 0)))
        {
            Id = 1
        };
        var exceed401 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 3, 9, 0, 0), new DateTime(2023, 4, 3, 17, 30, 0)))
        {
            Id = 5
        };
        var exceed402 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 4, 9, 0, 0), new DateTime(2023, 4, 4, 17, 30, 0)))
        {
            Id = 6
        };
        var exceed403 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 5, 9, 0, 0), new DateTime(2023, 4, 5, 17, 30, 0)))
        {
            Id = 7
        };
        var exceed404 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 7, 9, 0, 0), new DateTime(2023, 4, 7, 17, 30, 0)))
        {
            Id = 8
        };
        var exceed405 = new Registration(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 4, 7, 17, 01, 0), new DateTime(2023, 4, 7, 23, 59, 0)))
        {
            Id = 9
        };

        var timesheet = new Timesheet(reg1.TimeSlot.Start.Year, reg1.TimeSlot.Start.Month);
        employee.AddTimesheet(timesheet);

        employee.AddRegistration(reg1);
        employee.AddRegistration(exceed401);
        employee.AddRegistration(exceed402);
        employee.AddRegistration(exceed403);
        employee.AddRegistration(exceed404);
        var ex = Assert.Throws<AppException>(() => timesheet.RegistrationExceedsWeekLimitOf40Hours(exceed405.TimeSlot, exceed405.Id));
        Assert.True(ex.Message == "You exceeded the maximum of 40 hours for this week");
    }
}
