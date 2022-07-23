using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models;

public class UserPasswordChangeDto
{
    [JsonProperty("oldPassword")]
    [MinLength(6)]
    public string OldPassword { get; set; }

    [JsonProperty("newPassword")]
    [PasswordPropertyText(true)]
    public string NewPassword { get; set; }

    [Required]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; }
}