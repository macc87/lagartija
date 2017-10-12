using Utilities.Caching.HttpCaching.Core.Time;

namespace Utilities.Caching.HttpCaching.OutputCache.TimeAttributes
{
    public class CacheOutputUntilForHours : CacheOutputAttribute
    {
        /// <summary>
        ///     Cache item until absolute expiration today @ 17h45
        /// </summary>
        /// <param name="hours">17</param>
        public CacheOutputUntilForHours(int hours)
        {
            CacheTimeQuery = new UpToHours(hours);
        }
    }
}