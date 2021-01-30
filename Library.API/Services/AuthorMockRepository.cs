using Library.API.Data;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class AuthorMockRepository : IAuthorRepository
    {
        public void AddAuthor(AuthorDto author)
        {
            author.Id = Guid.NewGuid();
            LibraryMockData.Curent.Authors.Add(author);
        }

        public void DeleteAuthor(AuthorDto author)
        {
            LibraryMockData.Curent.Books.RemoveAll(book => book.AuthorId == author.Id);
            LibraryMockData.Curent.Authors.Remove(author);
        }

        public AuthorDto GetAuthor(Guid authorId)
        {
            var author = LibraryMockData.Curent.Authors.FirstOrDefault(a => a.Id == authorId);
            return author;
        }

        public IEnumerable<AuthorDto> GetAuthors()
        {
            return LibraryMockData.Curent.Authors;
        }

        public bool IsAuthorExists(Guid authorId)
        {
            return LibraryMockData.Curent.Authors.Any(a => a.Id == authorId);
        }
    }
}