using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Queries.HolidayQueries.GetAllHolidays;

public class GetAllHolidaysHandler : IRequestHandler<GetAllHolidaysQuery, IEnumerable<HolidayDTO>>
{
    private readonly IHolidayRepository _holidayRepository;
    public GetAllHolidaysHandler(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }
    public async Task<IEnumerable<HolidayDTO>> Handle(GetAllHolidaysQuery request, CancellationToken cancellationToken)
    {
        var holidays = await _holidayRepository.GetAll();
        var dtos = holidays.Select(holiday => ConvertToHolidayDTO.MapToHolidayDTO(holiday));
        return dtos;
    }
}
