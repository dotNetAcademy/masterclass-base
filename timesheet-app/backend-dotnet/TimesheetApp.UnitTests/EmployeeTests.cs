using AutoFixture;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.Enums;
using TimesheetApp.Domain.Models.ValueObjects;

namespace TimesheetApp.UnitTests;

public class EmployeeTests
{
    private readonly Fixture _fixture;

    public EmployeeTests()
    {
        _fixture = new Fixture();

        _fixture.Customize<Employee>(e => e.FromFactory(() => new Employee(
            _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<Role>(), _fixture.Create<string>())));
    }

    [Fact]
    public void GetEmployeesWithTimesheets()
    {
        // Arrange
        var employee = _fixture.Build<Employee>().Create();

        var registrations = new List<Registration>
        {
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 2, 8, 30, 0), new DateTime(2023, 10, 2, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 3, 8, 30, 0), new DateTime(2023, 10, 3, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 4, 8, 30, 0), new DateTime(2023, 10, 4, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 5, 8, 30, 0), new DateTime(2023, 10, 5, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 10, 6, 8, 30, 0), new DateTime(2023, 10, 6, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 6, 8, 30, 0), new DateTime(2023, 11, 6, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 7, 8, 30, 0), new DateTime(2023, 11, 7, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 8, 8, 30, 0), new DateTime(2023, 11, 8, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 9, 8, 30, 0), new DateTime(2023, 11, 9, 17, 00, 0))),
            new (RegistrationType.Workday, new TimeSlot(new DateTime(2023, 11, 10, 9, 30, 0), new DateTime(2023, 11, 10, 17, 00, 0))),
        };

        registrations.ForEach(r => employee.AddRegistration(r));

        var employeeDTO = EmployeeMapper.ToDto(employee);

        Assert.Equal(2, employeeDTO.Timesheets?.Count());
        Assert.Equal(40, employeeDTO.Timesheets?.First().TotalHours);
        Assert.Equal(39, employeeDTO.Timesheets?.Last().TotalHours);
    }
}
