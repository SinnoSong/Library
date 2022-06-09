using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.API.Entities
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = new Guid("E4A08DAF-1FF5-40B9-B4CA-F1ED5AC868BF"),
                    Name = "author1",
                    BirthDate = new DateTimeOffset(new DateTime(1993, 12, 30)),
                    BirthPlace = "上海",
                    Email = "author1@e.com"
                },
                new Author
                {
                    Id = new Guid("A6053251-D1C6-4B9C-86BE-A2FE8124A80F"),
                    Name = "author2",
                    BirthDate = new DateTimeOffset(new DateTime(1994, 12, 30)),
                    BirthPlace = "北京",
                    Email = "author2@e.com"
                },
                new Author
                {
                    Id = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990"),
                    Name = "author3",
                    BirthDate = new DateTimeOffset(new DateTime(1995, 12, 30)),
                    BirthPlace = "成都",
                    Email = "author3@e.com"
                });
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990"),
                    Title = "book1 Title",
                    Summary = "book1 Description",
                    Pages = 100,
                    Isbn="12345667",
                    AuthorId = new Guid("E4A08DAF-1FF5-40B9-B4CA-F1ED5AC868BF")
                },
                new Book
                {
                    Id = new Guid("D64263C7-A382-482C-B773-BA74CAA8F4A1"),
                    Title = "book2 Title",
                    Summary = "book2 Description",
                    Pages = 100,
                    Isbn = "12384298",
                    AuthorId = new Guid("A6053251-D1C6-4B9C-86BE-A2FE8124A80F")
                },
                new Book
                {
                    Id = new Guid("7C75C7C4-1268-4D60-999B-686B08D3B269"),
                    Title = "book3 Title",
                    Summary = "book3 Description",
                    Pages = 100,
                    AuthorId = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990")
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
                     FirstName = "System",
                     LastName = "Admin",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1")
                 },
                new User
                {
                    Id = "30a24107-d279-4e37-96fd-01af5b38cb27",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
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