using CSharpFunctionalExtensions;
using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Mappers;
using TimesheetApp.Domain.Interfaces.Repositories;

namespace TimesheetApp.Application.Queries.EmployeeQueries.GetEmployeesByName;

public class GetEmployeesByNameHandler : IRequestHandler<GetEmployeesByNameQuery, IEnumerable<EmployeeDTO>>
{
    private readonly IEmployeeRepository _employeeRepository;
    public GetEmployeesByNameHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDTO>> Handle(GetEmployeesByNameQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetByName(request.Name);
        var dtos = employees.Select(employee => ConvertToEmployeeDTOWithTimesheets.MapToEmployeeDTOWithTimesheets(employee));
        return dtos;
    }
}
