using NSubstitute;
using TimesheetApp.Domain.Exceptions;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Interfaces.Services;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Services;

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
            new (new DateTime(2024, 12, 25), "Christmas", Substitute.For<IValidateHoliday>())
        };

        _holidayRepository.GetByDate(default).ReturnsForAnyArgs(holidays);
        IValidateHoliday validateHoliday = new ValidateHoliday(_holidayRepository);

        var exception = Assert.Throws<AppException>(() =>
            new Holiday(new DateTime(2024, 12, 25), "Test", validateHoliday)
        );

        // Assert
        Assert.Equal("There is already a holiday registered on this day", exception.Message);
    }
}
