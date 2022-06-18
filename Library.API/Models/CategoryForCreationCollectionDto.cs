using System.Collections.Generic;

namespace Library.API.Models
{
    public class CategoryForCreationCollectionDto
    {
        public IEnumerable<CategoryForCreationDto> Categories { get; set; }
    }
}
