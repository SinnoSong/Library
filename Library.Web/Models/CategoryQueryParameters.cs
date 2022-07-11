using Newtonsoft.Json;

namespace Library.Web.Models;

public class CategoryQueryParameters : QueryParameters
{
    [JsonProperty("sort", Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
    public string Sort { get; set; } = "id";

    [JsonProperty("search", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public string? Search { get; set; }
}