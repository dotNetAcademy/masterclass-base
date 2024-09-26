using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.DTOs;

namespace TimesheetApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HolidaysController : ControllerBase
{
    private readonly IMediator _mediator;

    public HolidaysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize("read:holidays")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllHolidaysQuery());
            return Ok(result.ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("create")]
    [Authorize("write:holidays")]
    public async Task<IActionResult> CreateHoliday(HolidayDTO holidayDTO)
    {
        try
        {
            await _mediator.Send(new AddHolidayCommand(holidayDTO));
            return Ok(new { message = "Holiday succesful added" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
