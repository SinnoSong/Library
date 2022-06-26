using Newtonsoft.Json;

namespace Library.Web.Models
{
    public class AuthResponse
    {

        [JsonProperty("userId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty("accessToken", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("email", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }
    }
}
