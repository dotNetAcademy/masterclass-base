using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Commands.Registrations;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Queries.Registrations;
using TimesheetApp.Domain.Exceptions;

namespace TimesheetApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistrationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegistrationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize("read:registrations")]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetRegistrationsQuery(), HttpContext.RequestAborted);
        return Ok(result.ToList());
    }

    [HttpGet("{date}/{auth0Id}")]
    [Authorize("read:registrations")]
    public async Task<IActionResult> GetByDateAndEmployee(DateTime date, string auth0Id)
    {
        var registrations = await _mediator.Send(new GetRegistrationsByDateAndEmployeeQuery(date, auth0Id), HttpContext.RequestAborted);
        return Ok(registrations.ToList());
    }

    [HttpPut("edit")]
    [Authorize("write:registrations")]
    public async Task<IActionResult> UpdateRegistration(RegistrationDTO registrationDTO)
    {
        try
        {
            if (registrationDTO.Auth0Id != null)
            {
                await _mediator.Send(new EditRegistrationCommand(registrationDTO), HttpContext.RequestAborted);
                return Ok(new { message = "Timesheet Registration succesful added" });
            }
            return BadRequest("No Auth0Id provided");
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
