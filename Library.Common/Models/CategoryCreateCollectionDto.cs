namespace Library.Common.Models
{
    public class CategoryCreateCollectionDto
    {
        public CategoryCreateCollectionDto(IEnumerable<CategoryCreateDto> categories)
        {
            Categories = categories;
        }

        public IEnumerable<CategoryCreateDto> Categories { get; set; }
    }
}
