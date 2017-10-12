using System.Net.Http.Headers;
using System.Web.Http.Controllers;

namespace Utilities.Caching.HttpCaching.OutputCache
{
    public interface ICacheKeyGenerator
    {
        string MakeCacheKey(HttpActionContext context, MediaTypeHeaderValue mediaType, bool excludeQueryString = false, string[] excludeQueryStringFields = null);
    }
}
