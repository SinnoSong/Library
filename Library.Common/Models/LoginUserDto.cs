using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class LoginUserDto
    {
        [JsonProperty("userName", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string UserName { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Password { get; set; }
    }
}