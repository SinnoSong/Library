namespace Library.API.Models
{
    public class CategoryForCreationDto
    {
        public string Name { get; set; }
        public string? Summary { get; set; }

        public CategoryForCreationDto(string name, string? summary)
        {
            Name = name;
            Summary = summary;
        }
    }
}
