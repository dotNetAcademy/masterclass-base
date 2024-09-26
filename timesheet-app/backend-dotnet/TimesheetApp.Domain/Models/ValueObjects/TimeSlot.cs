using CSharpFunctionalExtensions;
using TimesheetApp.Domain.Exceptions;

namespace TimesheetApp.Domain.Models.ValueObjects
{
    public class TimeSlot : ValueObject
    {
        public TimeSlot()
        { }

        public TimeSlot(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new AppException("Start moment has to be earlier than end moment.");
            }
            if ((end - start).TotalHours - 0.5 > 8)
            {
                throw new AppException("This timeslot exceeds the maximum of 8 hours");
            }
            if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new AppException("Registration has to be on a weekday");
            }
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

        public double TotalHours
        {
            get
            {
                return (End - Start).TotalHours - 0.5;
            }
        }

        public void IsOverlapping(TimeSlot other)
        {
            if ((Start >= other.Start && Start <= other.End) || (End >= other.Start && End <= other.End))
            {
                throw new AppException("There is an overlap with another registration");
            }
        }
    }
}