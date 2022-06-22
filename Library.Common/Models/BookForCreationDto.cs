using System;

namespace Library.Common.Models
{
    public class BookForCreationDto
    {
        public string Title { get; set; }
        public string? Summary { get; set; }
        public decimal? Price { get; set; }
        public string? Isbn { get; set; }
        public string? Image { get; set; }
        public string Author { get; set; }
        public string Location { get; set; }
        public Guid CategoryId { get; set; }
        public int Pages { get; set; }

        public BookForCreationDto(string title, string author, string location)
        {
            Title = title;
            Author = author;
            Location = location;
        }
    }
}