using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models;

public class UserBase
{
    [Required]
    [MinLength(4)]
    [JsonProperty("userName")]
    public string UserName { get; set; }

    [JsonProperty("email")] [EmailAddress] public string Email { get; set; }
}