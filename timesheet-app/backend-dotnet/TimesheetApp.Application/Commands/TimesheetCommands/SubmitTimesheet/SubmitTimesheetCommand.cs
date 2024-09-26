using MediatR;

public record SubmitTimesheetCommand(int Id) : IRequest;
