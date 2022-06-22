using Library.Common.Models;
using System.Collections.Generic;

namespace Library.API.Entities
{
    public class BookForCreationCollectionDto
    {
        public IEnumerable<BookForCreationDto> Books { get; set; }
    }
}