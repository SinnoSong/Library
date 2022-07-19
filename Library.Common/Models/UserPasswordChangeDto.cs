

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class UserPasswordChangeDto
    {
        [JsonProperty("oldPassword")]
        [MinLength(6)]
        public string OldPassword { get; set; }
        [JsonProperty("newPassword")]
        [MinLength(6)]
        public string NewPassword { get; set; }

    }
}
