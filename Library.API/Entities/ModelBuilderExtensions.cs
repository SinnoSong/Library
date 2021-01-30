using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Entities
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = new Guid("E4A08DAF-1FF5-40B9-B4CA-F1ED5AC868BF"),
                    Name = "author1",
                    BirthData = new DateTimeOffset(new DateTime(1993, 12, 30)),
                    BirthPlace = "上海",
                    Email = "author1@e.com"
                },
                new Author
                {
                    Id = new Guid("A6053251-D1C6-4B9C-86BE-A2FE8124A80F"),
                    Name = "author2",
                    BirthData = new DateTimeOffset(new DateTime(1994, 12, 30)),
                    BirthPlace = "北京",
                    Email = "author2@e.com"
                },
                new Author
                {
                    Id = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990"),
                    Name = "author3",
                    BirthData = new DateTimeOffset(new DateTime(1995, 12, 30)),
                    BirthPlace = "成都",
                    Email = "author3@e.com"
                });
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990"),
                    Title = "book1 Title",
                    Description = "book1 Description",
                    Pages = 100,
                    AuthorId = new Guid("E4A08DAF-1FF5-40B9-B4CA-F1ED5AC868BF")
                },
                new Book
                {
                    Id = new Guid("D64263C7-A382-482C-B773-BA74CAA8F4A1"),
                    Title = "book2 Title",
                    Description = "book2 Description",
                    Pages = 100,
                    AuthorId = new Guid("A6053251-D1C6-4B9C-86BE-A2FE8124A80F")
                },
                new Book
                {
                    Id = new Guid("7C75C7C4-1268-4D60-999B-686B08D3B269"),
                    Title = "book3 Title",
                    Description = "book3 Description",
                    Pages = 100,
                    AuthorId = new Guid("058DDAC3-C6F5-42C2-A8EC-4A8000802990")
                });
        }
    }
}