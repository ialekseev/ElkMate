using System;

namespace SmartElk.ElkMate.Common
{
    public struct TimeRange
    {
        public TimeSpan From { get; private set; }
        public TimeSpan To { get; private set; }

        public TimeRange(TimeSpan @from, TimeSpan to) : this()
        {
            From = @from;
            To = to;
        }
        
        public bool IsBetween(TimeSpan timeSpan)
        {
            var now = timeSpan;
            if (From < To)
                return From <= now && now <= To;
            return !(To < now && now < From);
        }

        public bool IsBetween(DateTime datetime)
        {
            return IsBetween(datetime.TimeOfDay);
        }
    }
}
