using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class RegisterUser : UserDto
    {

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


        [JsonProperty("grade")] public byte Grade { get; set; }
    }
}