using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Domain.Exceptions;

namespace TimesheetApp.Domain.Models.ValueObjects
{
    public class TimeSlot : ValueObject
    {
        public TimeSlot() { }

        public TimeSlot(DateTime start, DateTime end)
        {
            if (start > end) { throw new AppException("Start moment has to be earlier than end moment."); }
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
