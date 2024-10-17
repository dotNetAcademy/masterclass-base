using CSharpFunctionalExtensions;
using MediatR;
using TimesheetApp.Application.DTOs;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Mappers;

namespace TimesheetApp.Application.Queries.Employees;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDTO>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var result = await _employeeRepository.GetAll(cancellationToken);
        var dtos = result.Select(employee => EmployeeMapper.ToDto(employee));
        return dtos;
    }
}

public record GetEmployeesQuery() : IRequest<IEnumerable<EmployeeDTO>>;
