using Newtonsoft.Json;

namespace Library.API.Web.Models
{
    public class AuthResponse
    {

        [JsonProperty("userId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty("token", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("email", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

    }
}
