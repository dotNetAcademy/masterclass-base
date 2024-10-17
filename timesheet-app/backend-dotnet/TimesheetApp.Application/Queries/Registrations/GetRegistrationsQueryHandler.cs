using MediatR;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Queries.Registrations;

public class GetRegistrationsQueryHandler : IRequestHandler<GetRegistrationsQuery, IEnumerable<Registration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetRegistrationsQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<IEnumerable<Registration>> Handle(GetRegistrationsQuery request, CancellationToken cancellationToken)
    {
        return await _registrationRepository.GetAllAsync(cancellationToken);
    }
}

public record GetRegistrationsQuery() : IRequest<IEnumerable<Registration>>;
