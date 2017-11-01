using Newtonsoft.Json;

namespace Fantasy.API.DataAccess.Models.Services.Survey
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
