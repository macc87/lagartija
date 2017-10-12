using Utilities.Caching.HttpCaching.Core.Time;

namespace Utilities.Caching.HttpCaching.OutputCache.TimeAttributes
{
    public class CacheOutputUntilToday : CacheOutputAttribute
    {
        /// <summary>
        ///     Cache item until absolute expiration today @ 17h45
        /// </summary>
        /// <param name="hour">17</param>
        /// <param name="minute">45</param>
        /// <param name="second">0</param>
        public CacheOutputUntilToday(int hour = 23,
                                     int minute = 59,
                                     int second = 59)
        {
            CacheTimeQuery = new ThisDay(hour, minute, second);
        }
    }
}