using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class CategoryDto
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("summary", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Summary { get; set; }

        public CategoryDto(Guid id, string name, string? summary)
        {
            Id = id;
            Name = name;
            Summary = summary;
        }
    }
}