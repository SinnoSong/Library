using Newtonsoft.Json;

namespace Library.Web.Models
{
    public class QueryParameters
    {
        [JsonProperty("page")]
        public int Page { get; set; } = 1;
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 25;
    }
}
