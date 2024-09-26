using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Queries.HolidayQueries.GetHolidaysById;

public class GetHolidaysByDateHandler : IRequestHandler<GetHolidaysByDateQuery, IEnumerable<HolidayDTO>>
{
    private readonly IHolidayRepository _holidayRepository;
    public GetHolidaysByDateHandler(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
    }
    public async Task<IEnumerable<HolidayDTO>> Handle(GetHolidaysByDateQuery request, CancellationToken cancellationToken)
    {
        var holidays = await _holidayRepository.GetByDate(request.Date);
        var dtos = holidays.Select(holiday => ConvertToHolidayDTO.MapToHolidayDTO(holiday));
        return dtos;
    }
}
