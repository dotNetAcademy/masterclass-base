using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _mediator.Send(new GetEmployeeByAuth0IdQuery(auth0Id));
            var id = result.Id;
            var timesheets = await _mediator.Send(new GetTimesheetsByEmployeeIdQuery(id));
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
            await _mediator.Send(new SubmitTimesheetCommand(id));
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
            await _mediator.Send(new ApproveTimesheetCommand(id));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
