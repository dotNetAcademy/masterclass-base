using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Mappers;

namespace TimesheetApp.Application.Queries.Holidays;

public class GetHolidaysByDateQueryHandler : IRequestHandler<GetHolidaysByDateQuery, IEnumerable<HolidayDTO>>
{
    private readonly IHolidayRepository _holidayRepository;
    public GetHolidaysByDateQueryHandler(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }
    public async Task<IEnumerable<HolidayDTO>> Handle(GetHolidaysByDateQuery request, CancellationToken cancellationToken)
    {
        var holidays = await _holidayRepository.GetByDate(request.Date, cancellationToken);
        var dtos = holidays.Select(holiday => HolidayMapper.ToDto(holiday));
        return dtos;
    }
}

public record GetHolidaysByDateQuery(DateTime Date) : IRequest<IEnumerable<HolidayDTO>>;
