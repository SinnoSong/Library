using Newtonsoft.Json;

namespace Library.Web.Models;

public class LendRecordQueryParameters : QueryParameters
{
    [JsonProperty("sort")]
    public string? Sort { get; set; }
    [JsonProperty("userId")]
    public Guid? UserId { get; set; }
    [JsonProperty("lendTime")]
    public string? LendTime { get; set; }
    [JsonProperty("returnTime")]
    public string? ReturnTime { get; set; }
}