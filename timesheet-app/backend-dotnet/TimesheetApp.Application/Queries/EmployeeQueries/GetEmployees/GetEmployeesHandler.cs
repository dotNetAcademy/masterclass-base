using CSharpFunctionalExtensions;
using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Queries.EmployeeQueries.GetEmployees;

public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDTO>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var result = await _employeeRepository.GetAll();
        var dtos = result.Select(employee => ConvertToEmployeeDTOWithTimesheets.MapToEmployeeDTOWithTimesheets(employee));
        return dtos;
    }
}
