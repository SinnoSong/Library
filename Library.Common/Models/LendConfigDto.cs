using Newtonsoft.Json;

namespace Library.Common.Models;

public class LendConfigDto
{
    [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public Guid Id { get; set; }

    [JsonProperty("readerGrade", Required = Required.Always)]
    public byte ReaderGrade { get; set; }

    [JsonProperty("maxLendNumber", Required = Required.Always)]
    public int MaxLendNumber { get; set; }

    [JsonProperty("maxLendDays", Required = Required.Always)]
    public int MaxLendDays { get; set; }
}