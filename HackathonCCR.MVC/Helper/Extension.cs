using System;

namespace HackathonCCR
{
    public static class Extension
    {
        public static DateTime Brasilia(this DateTime data)
        {
            var utc = DateTime.UtcNow;
            var brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var dateTimeNowBrasilia = TimeZoneInfo.ConvertTimeFromUtc(utc, brasiliaTimeZone);

            return dateTimeNowBrasilia;
        }
    }
}
