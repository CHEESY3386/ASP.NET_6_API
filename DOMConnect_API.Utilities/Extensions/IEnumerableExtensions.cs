namespace DOMConnect_API.Utilities.Extensions
{
    public static class IEnumerableExtensions
    {
        public static double RssiAvg(this IEnumerable<int> rssiSignals)
        {
            return 2 * (rssiSignals.Average() + 100);
        }
    }
}
