using MediatR;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Queries.Employees;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetById(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Employee not found");
    }
}

public record GetEmployeeByIdQuery(string Id) : IRequest<Employee>;
