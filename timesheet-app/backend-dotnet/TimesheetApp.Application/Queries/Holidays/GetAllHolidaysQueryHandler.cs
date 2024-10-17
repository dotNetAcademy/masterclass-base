using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Mappers;

namespace TimesheetApp.Application.Queries.Holidays;

public class GetAllHolidaysQueryHandler : IRequestHandler<GetAllHolidaysQuery, IEnumerable<HolidayDTO>>
{
    private readonly IHolidayRepository _holidayRepository;
    public GetAllHolidaysQueryHandler(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }
    public async Task<IEnumerable<HolidayDTO>> Handle(GetAllHolidaysQuery request, CancellationToken cancellationToken)
    {
        var holidays = await _holidayRepository.GetAll(cancellationToken);
        var dtos = holidays.Select(holiday => HolidayMapper.ToDto(holiday));
        return dtos;
    }
}

public record GetAllHolidaysQuery() : IRequest<IEnumerable<HolidayDTO>>;

