using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models;

public class RegisterAdmin : UserBase
{
    [JsonProperty("password")]
    [MinLength(6)]
    public string Password { get; set; }

    [Required] [Compare(nameof(Password))] public string ConfirmPassword { get; set; }
}