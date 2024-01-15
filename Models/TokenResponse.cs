using Newtonsoft.Json;

namespace CustomerSurveySystem.Models
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("forceChangePassword")]
        public string ForceChangePassword { get; set; }

        [JsonProperty("cops")]
        public string Cops { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("serverInstanceIdentity")]
        public string ServerInstanceIdentity { get; set; }

        [JsonProperty(".issued")]
        public string Issued { get; set; }

        [JsonProperty(".expires")]
        public string Expires { get; set; }
    }
}
