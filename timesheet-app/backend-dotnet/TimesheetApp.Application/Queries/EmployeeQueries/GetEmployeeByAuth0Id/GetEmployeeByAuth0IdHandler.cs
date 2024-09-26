using MediatR;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Queries.EmployeeQueries.GetEmployeeByAuth0Id;

public class GetEmployeeByAuth0IdHandler : IRequestHandler<GetEmployeeByAuth0IdQuery, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByAuth0IdHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(GetEmployeeByAuth0IdQuery request, CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetByAuth0Id(request.Auth0Id)
            ?? throw new KeyNotFoundException("Employee not found");
    }
}
