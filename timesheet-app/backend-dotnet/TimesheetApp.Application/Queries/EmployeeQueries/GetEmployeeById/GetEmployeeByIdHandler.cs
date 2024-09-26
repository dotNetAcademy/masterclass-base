using MediatR;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Queries.EmployeeQueries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetById(request.Id)
            ?? throw new KeyNotFoundException("Employee not found");
    }
}
