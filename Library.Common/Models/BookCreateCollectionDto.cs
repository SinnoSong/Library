namespace Library.Common.Models
{
    public class BookCreateCollectionDto
    {
        public BookCreateCollectionDto(IEnumerable<BookCreateDto> books)
        {
            Books = books;
        }

        public IEnumerable<BookCreateDto> Books { get; set; }
    }
}