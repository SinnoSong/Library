namespace Library.Common.Models
{
    public class CategoryForCreationCollectionDto
    {
        public CategoryForCreationCollectionDto(IEnumerable<CategoryCreateDto> categories)
        {
            Categories = categories;
        }

        public IEnumerable<CategoryCreateDto> Categories { get; set; }
    }
}
