using TimesheetApp.Application.DTOs;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Mappers;

public class ConvertToRegistrationDTO
{
    public static RegistrationDTO MapRegistrationToDTO(Registration registration, bool isTimesheetApproved, string? auth0Id = null)
    {
        return new RegistrationDTO()
        {
            Id = registration.Id,
            RegistrationType = registration.RegistrationType,
            Start = registration.TimeSlot.Start,
            End = registration.TimeSlot.End,
            IsTimesheetApproved = isTimesheetApproved,
            Auth0Id = auth0Id,
            TotalHours = registration.TimeSlot.TotalHours,
        };
    }
}
