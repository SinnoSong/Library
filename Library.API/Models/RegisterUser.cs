using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class RegisterUser
    {
        [Required, MinLength(4)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
        public byte Grade { get; set; }
    }
}