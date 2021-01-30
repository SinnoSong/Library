using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "必须提供姓名")]
        [MaxLength(20, ErrorMessage = "姓名最长为20个字符")]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }

        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
    }
}