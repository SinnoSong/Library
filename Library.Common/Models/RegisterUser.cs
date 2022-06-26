using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Library.Common.Models
{
    public class RegisterUser
    {
        [Required, MinLength(4)]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("email")][EmailAddress] public string Email { get; set; }

        [JsonProperty("password")]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }


        [JsonProperty("grade")] public byte Grade { get; set; }
    }
}