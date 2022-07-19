using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class UserEmailChangeDto
    {
        [JsonProperty("email")][EmailAddress] public string Email { get; set; }

    }
}
