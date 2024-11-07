using CSharpFunctionalExtensions;
using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Mappers;

namespace TimesheetApp.Application.Queries.Employees;

public class GetEmployeesByNameQueryHandler : IRequestHandler<GetEmployeesByNameQuery, IEnumerable<EmployeeDTO>>
{
    private readonly IEmployeeRepository _employeeRepository;
    public GetEmployeesByNameQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDTO>> Handle(GetEmployeesByNameQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetByName(request.Name, cancellationToken);
        var dtos = employees.Select(employee => EmployeeMapper.ToDto(employee));
        return dtos;
    }
}

public record GetEmployeesByNameQuery(string Name) : IRequest<IEnumerable<EmployeeDTO>>;
