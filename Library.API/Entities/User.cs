using Microsoft.AspNetCore.Identity;
using System;

namespace Library.API.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }
}