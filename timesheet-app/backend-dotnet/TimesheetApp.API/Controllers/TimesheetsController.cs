using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Commands.Timesheets;
using TimesheetApp.Application.Queries.Timesheets;

namespace TimesheetApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimesheetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TimesheetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{auth0Id}")]
    [Authorize("read:timesheets")]
    public async Task<IActionResult> GetTimesheetsByEmployeeId(string auth0Id)
    {
        try
        {
            var timesheets = await _mediator.Send(new GetTimesheetsByEmployeeIdQuery(auth0Id), HttpContext.RequestAborted);
            return Ok(timesheets.ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("submit/{id}")]
    [Authorize("submit:timesheets")]
    public async Task<IActionResult> SubmitTimesheet(int id)
    {
        try
        {
            await _mediator.Send(new SubmitTimesheetCommand(id), HttpContext.RequestAborted);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("approve/{id}")]
    [Authorize("approve:timesheets")]
    public async Task<IActionResult> ApproveTimesheet(int id)
    {
        try
        {
            await _mediator.Send(new ApproveTimesheetCommand(id), HttpContext.RequestAborted);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
