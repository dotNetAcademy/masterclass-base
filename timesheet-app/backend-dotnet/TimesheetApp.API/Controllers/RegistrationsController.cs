using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.DTOs;
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
        var result = await _mediator.Send(new GetRegistrationsQuery());
        return Ok(result.ToList());
    }

    [HttpGet("{date}/{auth0Id}")]
    [Authorize("read:registrations")]
    public async Task<IActionResult> GetByDateAndEmployee(DateTime date, string auth0Id)
    {
        var result = await _mediator.Send(new GetEmployeeByAuth0IdQuery(auth0Id));
        var id = result.Id;
        var registrations = await _mediator.Send(new GetRegistrationsByDateAndEmployeeQuery(date, id, auth0Id));
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
                var result = await _mediator.Send(new GetEmployeeByAuth0IdQuery(registrationDTO.Auth0Id));
                var id = result.Id;
                await _mediator.Send(new EditRegistrationCommand(id, registrationDTO));
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
