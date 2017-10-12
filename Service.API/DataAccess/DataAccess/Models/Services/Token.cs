using Newtonsoft.Json;

namespace DataAccess.Models.Services.Survey
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
