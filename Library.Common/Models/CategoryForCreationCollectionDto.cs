using System.Collections.Generic;

namespace Library.Common.Models
{
    public class CategoryForCreationCollectionDto
    {
        public IEnumerable<CategoryForCreationDto> Categories { get; set; }
    }
}
