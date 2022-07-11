using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class CategoryCreateDto
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("summary", Required = Required.Always)]
        public string? Summary { get; set; }

        public CategoryCreateDto(string name, string? summary)
        {
            Name = name;
            Summary = summary;
        }

        public CategoryCreateDto()
        {
        }
    }
}