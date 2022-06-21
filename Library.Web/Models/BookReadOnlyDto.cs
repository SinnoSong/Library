using Newtonsoft.Json;

namespace Library.Web.Models
{
    public class BookReadOnlyDto
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }
        [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty("summary", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Summary { get; set; }
        [JsonProperty("price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }
        [JsonProperty("isbn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Isbn { get; set; }
        [JsonProperty("image", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Image { get; set; }
        [JsonProperty("pages", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int Pages { get; set; }
        [JsonProperty("author", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }
        [JsonProperty("location", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }
        [JsonProperty("isLend", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsLend { get; set; }
        [JsonProperty("categoryId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid CategoryId { get; set; }

        public BookReadOnlyDto(Guid id, string title, string? summary, decimal? price, string? isbn, string? image, int pages, string author, string location, bool isLend, Guid categoryId)
        {
            Id = id;
            Title = title;
            Summary = summary;
            Price = price;
            Isbn = isbn;
            Image = image;
            Pages = pages;
            Author = author;
            Location = location;
            IsLend = isLend;
            CategoryId = categoryId;
        }

        public BookReadOnlyDto()
        {
        }
    }
}
