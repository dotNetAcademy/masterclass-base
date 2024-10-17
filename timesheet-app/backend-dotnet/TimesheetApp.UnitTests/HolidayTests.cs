using NSubstitute;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Interfaces.Validators;
using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Validators;

namespace TimesheetApp.UnitTests;

public class HolidayTests
{
    private readonly IHolidayRepository _holidayRepository;

    public HolidayTests()
    {
        _holidayRepository = Substitute.For<IHolidayRepository>();
    }

    [Fact]
    public void BlockAddingDuplicateHoliday()
    {
        var holidays = new List<Holiday>()
        {
            new (new DateTime(2024, 12, 25), "Christmas")
        };

        _holidayRepository.GetByDate(default, CancellationToken.None).ReturnsForAnyArgs(holidays);
        IHolidayValidator validateHoliday = new HolidayValidator();

        var exception = Assert.Throws<AppException>(() =>
            new Holiday(new DateTime(2024, 12, 25), "Test")
        );

        // Assert
        Assert.Equal("There is already a holiday registered on this day", exception.Message);
    }
}
