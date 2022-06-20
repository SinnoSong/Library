using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Web.Models
{
    public class LoginUserDto
    {
        [JsonProperty("email", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Email { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Password { get; set; }
    }
}
