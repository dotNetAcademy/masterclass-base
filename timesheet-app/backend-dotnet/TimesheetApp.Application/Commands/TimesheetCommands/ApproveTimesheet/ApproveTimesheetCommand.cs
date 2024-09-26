using MediatR;

public record ApproveTimesheetCommand(int Id) : IRequest;
