using MediatR;
using TimesheetApp.Domain.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Queries.RegistrationQueries.GetRegistrations;

public class GetRegistrationsHandler : IRequestHandler<GetRegistrationsQuery, IEnumerable<Registration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetRegistrationsHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<IEnumerable<Registration>> Handle(GetRegistrationsQuery request, CancellationToken cancellationToken)
    {
        return await _registrationRepository.GetAllAsync();
    }
}
