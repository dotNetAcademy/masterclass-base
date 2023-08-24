using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Domain.Models;

public record GetEmployeeByIdQuery(string Id) : IRequest<Employee>;
