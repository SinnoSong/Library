using Library.API.Models;
using System;
using System.Collections.Generic;

namespace Library.API.Data
{
    public class LibraryMockData
    {
        public static LibraryMockData Curent { get; } = new LibraryMockData();
        public List<AuthorDto> Authors { get; set; }
        public List<BookDto> Books { get; set; }

        public LibraryMockData()
        {
            Authors = new List<AuthorDto>
            {
                new AuthorDto{Id = new Guid("6842D019-8A2A-43F0-A1C0-1748EC3C6EBA"),
                Name = "Author 1",Age = 40,Email="author1@xx.com"
                },new AuthorDto{Id = new Guid("99B156B3-2D45-4F6B-9A35-2277E631332A"),
                Name = "Author 2",Age = 45,Email="author2@xx.com"
                }
            };
            Books = new List<BookDto>
            {
                new BookDto{Id = new Guid("3706609F-A0EC-4BC4-9FCF-9CC5F0AAB724"),
                    Title = "Book1",Description="Book1 Description",Pages=290,AuthorId=new Guid("6842D019-8A2A-43F0-A1C0-1748EC3C6EBA") },
                new BookDto{Id = new Guid("E4EFDF0F-E96C-446D-9C2B-47578A1B5D24"),
                    Title = "Book2",Description="Book2 Description",Pages=190,AuthorId=new Guid("6842D019-8A2A-43F0-A1C0-1748EC3C6EBA") },
                new BookDto{Id = new Guid("CBE8E492-DE9A-4F10-ADE2-B873D0416A3C"),
                    Title = "Book3",Description="Book3 Description",Pages=690,AuthorId=new Guid("99B156B3-2D45-4F6B-9A35-2277E631332A") },
                new BookDto{Id = new Guid("ECFAFACE-F5E6-4A1E-8724-A536FDEEB241"),
                    Title = "Book4",Description="Book4 Description",Pages=239,AuthorId=new Guid("99B156B3-2D45-4F6B-9A35-2277E631332A") }
            };
        }
    }
}