using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Application.DTOs;

public record AddRegistrationCommand(string EmployeeId, AddRegistrationDTO AddRegistrationDTO) : IRequest;
