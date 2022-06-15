using System;

namespace Library.API.Models
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Summary { get; set; }
        public decimal? Price { get; set; }
        public string? Isbn { get; set; }
        public string? Image { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }
        public string Location { get; set; }
        public bool IsLend { get; set; }
        public Guid CategoryId { get; set; }

        #region ctor

        public BookDto(string title, string author, string location)
        {
            Title = title;
            Author = author;
            Location = location;
        }

        public BookDto()
        {
        }
        #endregion
    }
}