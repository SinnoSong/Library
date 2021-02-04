using Microsoft.AspNetCore.Identity;
using System;

namespace Library.API.Entities
{
    public class User : IdentityUser
    {
        public DateTimeOffset BirthDate { get; set; }
    }
}