using Newtonsoft.Json;

namespace Library.Web.Models;

public class LendRecordQueryParameters : QueryParameters
{
    [JsonProperty("sort")] public string? Sort { get; set; } = "id";
    [JsonProperty("userId")] public Guid? UserId { get; set; }
    [JsonProperty("lendTime")] public DateTime? LendTime { get; set; }
    [JsonProperty("returnTime")] public DateTime? ReturnTime { get; set; }
}