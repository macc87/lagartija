using System.Web.Http;

namespace Utilities.Caching.HttpCaching.OutputCache
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        public static CacheOutputConfiguration CacheOutputConfiguration(this HttpConfiguration config)
        {
            return new CacheOutputConfiguration(config);
        }
    }
}