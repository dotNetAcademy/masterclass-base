using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Commands.Registrations;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Queries.Employees;
using TimesheetApp.Domain.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimesheetApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<EmployeesController>
    [Authorize("read:employees")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetEmployeesQuery(), HttpContext.RequestAborted);
        return Ok(result.ToList());
    }

    // GET api/<EmployeesController>/5
    [HttpGet("{id}")]
    [Authorize("read:employees")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id), HttpContext.RequestAborted);
        return Ok(result);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _mediator.Send(new GetEmployeesByNameQuery(name), HttpContext.RequestAborted);
        return Ok(result);
    }

    [HttpPost("AddRegistration")]
    [Authorize("write:registrations")]
    public async Task<IActionResult> AddRegistration(AddRegistrationDTO addRegistrationDTO)
    {
        try
        {
            await _mediator.Send(new AddRegistrationCommand(addRegistrationDTO), HttpContext.RequestAborted);
            return Ok(new { message = "Registration succesful added" });
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST api/<EmployeesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<EmployeesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<EmployeesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
