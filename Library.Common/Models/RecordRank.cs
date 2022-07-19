using Newtonsoft.Json;

namespace Library.Common.Models;

public class RecordRank
{
    [JsonProperty("rank")]
    public int Rank { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("total")]
    public string Total { get; set; }
}