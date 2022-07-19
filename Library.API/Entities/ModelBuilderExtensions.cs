using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Entities;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book("book1 Title", "马克思", "2FC1005")
            {
                Id = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990"),
                Summary = "book1 Description",
                Pages = 100,
                Isbn = "12345667"
            },
            new Book("book2 Title", "毛泽东", "2FC1004")
            {
                Id = new Guid("D64263C7-A382-482C-B773-BA74CAA8F4A1"),
                Summary = "book2 Description",
                Pages = 100,
                Isbn = "12384298"
            },
            new Book("book3 Title", "Jon Asa", "2FC1003")
            {
                Id = new Guid("7C75C7C4-1268-4D60-999B-686B08D3B269"),
                Summary = "book3 Description",
                Pages = 100
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
            },
            new Role
            {
                Name = "SuperAdministrator",
                NormalizedName = "SUPERADMINISTRATOR",
                Id = "BCC4BFC0-6252-4A32-B5B8-9005E4560402"
            }
        );

        var hasher = new PasswordHasher<User>();
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "8e448afa-f008-446e-a52f-13c449803c2e",
                Email = "admin@library.com",
                NormalizedEmail = "ADMIN@LIBRARY.COM",
                UserName = "admin@library.com",
                NormalizedUserName = "ADMIN@LIBRARY.COM",
                PasswordHash = hasher.HashPassword(null, "12345678"),
                // 设置等级
                Grade = 1
            },
            new User
            {
                Id = "30a24107-d279-4e37-96fd-01af5b38cb27",
                Email = "user@library.com",
                NormalizedEmail = "USER@LIBRARY.COM",
                UserName = "user@library.com",
                NormalizedUserName = "USER@LIBRARY.COM",
                PasswordHash = hasher.HashPassword(null, "12345678")
            },
            new User
            {
                Id = "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                Email = "super@library.com",
                NormalizedEmail = "SUPER@LIBRARY.COM",
                UserName = "super@library.com",
                NormalizedUserName = "SUPER@LIBRARY.COM",
                PasswordHash = hasher.HashPassword(null, "12345678")
            });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                UserId = "30a24107-d279-4e37-96fd-01af5b38cb27"
            },
            new IdentityUserRole<string>
            {
                RoleId = "2093BF77-ED2E-453B-911F-325BC99C4504",
                UserId = "8e448afa-f008-446e-a52f-13c449803c2e"
            },
            new IdentityUserRole<string>
            {
                RoleId = "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                UserId = "32A61BDD-DF3C-4B71-9444-F5730F8A619B"
            });
    }
}