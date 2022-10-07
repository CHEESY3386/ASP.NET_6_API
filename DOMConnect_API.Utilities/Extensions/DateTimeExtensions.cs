namespace DOMConnect_API.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundUpToQuarterHour(this DateTime dateTime)
        {
            var timeSpan = TimeSpan.FromMinutes(15);

            return new DateTime((dateTime.Ticks + timeSpan.Ticks - 1) / timeSpan.Ticks * timeSpan.Ticks, dateTime.Kind);
        }
    }
}
