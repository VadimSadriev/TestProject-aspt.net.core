using System;
using System.Text.RegularExpressions;

namespace TestProject.Core
{
    public static class StringExtensions
    {
        public static bool IsEmail(this string str)
        {
            return Regex.IsMatch(str, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        public static bool IsValidUrl(this string str)
        {
            return Regex.IsMatch(str, @"[-a-zA-Z0-9@:% _\+.~#?&//=]{2,256}\.[a-z]{2,4}\b(\/[-a-zA-Z0-9@:%_\+.~#?&//=]*)?");
        }

        public static DateTime ToDateTime(this string str)
        {
            try
            {
                return DateTime.Parse(str);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static TimeSpan ToTimeSpan(this string str)
        {
            try
            {
                var dateTime = DateTime.Parse(str);
                return new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
            }
            catch
            {
                return new TimeSpan();
            }
        }
    }
}
