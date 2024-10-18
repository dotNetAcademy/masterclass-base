using AutoFixture;
using NSubstitute;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.Enums;
using TimesheetApp.Domain.Models.ValueObjects;
using TimesheetApp.Domain.Validators;

namespace TimesheetApp.UnitTests;

public class TimesheetTests
{
    private readonly Fixture _fixture;
    private readonly IHolidayRepository _holidayRepository;

    public TimesheetTests()
    {
        _fixture = new Fixture();
        _fixture.Customize<Employee>(e => e.FromFactory(() => new Employee(
            _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<Role>(), _fixture.Create<string>())));
        _holidayRepository = Substitute.For<IHolidayRepository>();
    }

    [Fact]
    public void GetTimesheetsWithRegistrationsByWeek()
    {
        var employee = _fixture.Build<Employee>().Create();

        var registrations = new List<Registration>
        {
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 2, 8, 30, 0), new DateTime(2023, 10, 2, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 3, 8, 30, 0), new DateTime(2023, 10, 3, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 4, 8, 30, 0), new DateTime(2023, 10, 4, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 5, 8, 30, 0), new DateTime(2023, 10, 5, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 6, 8, 30, 0), new DateTime(2023, 10, 6, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 6, 8, 30, 0), new DateTime(2023, 11, 6, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 7, 8, 30, 0), new DateTime(2023, 11, 7, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 8, 8, 30, 0), new DateTime(2023, 11, 8, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 9, 8, 30, 0), new DateTime(2023, 11, 9, 17, 00, 0))),
            new(RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 10, 9, 30, 0), new DateTime(2023, 11, 10, 17, 00, 0))),
        };
        var validateRegistration = new RegistrationValidator();

        registrations.ForEach(r => employee.AddRegistration(r));

        var dtos = employee.Timesheets.Select(t => TimesheetMapper.ToDto(t));

        Assert.Equal(2, dtos.Count());
        Assert.Equal(40, dtos.First().TotalHours);
        Assert.Equal(39, dtos.Last().TotalHours);
    }
}
