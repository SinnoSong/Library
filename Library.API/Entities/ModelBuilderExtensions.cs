using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.API.Entities
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book("book1 Title", "马克思")
                {
                    Id = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990"),
                    Summary = "book1 Description",
                    Pages = 100,
                    Isbn = "12345667"
                },
                new Book("book2 Title", "毛泽东")
                {
                    Id = new Guid("D64263C7-A382-482C-B773-BA74CAA8F4A1"),
                    Summary = "book2 Description",
                    Pages = 100,
                    Isbn = "12384298"
                },
                new Book("book3 Title", "Jon Asa")
                {
                    Id = new Guid("7C75C7C4-1268-4D60-999B-686B08D3B269"),
                    Summary = "book3 Description",
                    Pages = 100,
                });
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "77967ECF-0A18-4993-A243-FDCC86F7EC1B"
                },
                new Role
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "2093BF77-ED2E-453B-911F-325BC99C4504"
                }
                );

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     Id = "8e448afa-f008-446e-a52f-13c449803c2e",
                     Email = "admin@bookstore.com",
                     NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                     UserName = "admin@bookstore.com",
                     NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1")
                 },
                new User
                {
                    Id = "30a24107-d279-4e37-96fd-01af5b38cb27",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                    UserId = "30a24107-d279-4e37-96fd-01af5b38cb27"
                }, new IdentityUserRole<string>
                {
                    RoleId = "2093BF77-ED2E-453B-911F-325BC99C4504",
                    UserId = "8e448afa-f008-446e-a52f-13c449803c2e"
                });
        }
    }
}