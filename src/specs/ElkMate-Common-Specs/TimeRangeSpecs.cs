// ReSharper disable InconsistentNaming

using System;
using System.Globalization;
using NUnit.Framework;

namespace SmartElk.ElkMate.Common.Specs
{
    public class TimeRangeSpecs
    {
        [TestFixture]
        public class when_trying_to_check_if_value_in_range
        {
            [TestCase("17:34", "19:59", "2009-05-08 17:50", Result = true)]
            [TestCase("17:34", "19:59", "2009-05-08 17:33", Result = false)]
            [TestCase("23:30", "00:15", "2009-05-08 00:02", Result = true)]
            [TestCase("23:30", "00:15", "2009-05-08 00:16", Result = false)]            
            public bool should_validate_properly(string fromStr, string toStr, string dateStr)
            {
                //arrange
                var from = DateTime.ParseExact(fromStr, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                var to = DateTime.ParseExact(toStr, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                var date = DateTime.ParseExact(dateStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                var range = new TimeRange(from, to);

                //act
                return range.IsBetween(date);
            }
        }
    }
}

// ReSharper restore InconsistentNaming